using System.Reflection;
using System.Reflection.Emit;

namespace ReflectionArticle07DynamicIL
{
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            // Define a dynamic assembly
            AssemblyName assemblyName = new AssemblyName("DynamicAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                assemblyName, AssemblyBuilderAccess.RunAndCollect);

            // Define a dynamic module within the assembly
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");

            // Define a dynamic type
            TypeBuilder typeBuilder = moduleBuilder.DefineType("Calculator", TypeAttributes.Public);

            // Define a dynamic method within the type
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                "Add",
                MethodAttributes.Public | MethodAttributes.Static,
                typeof(int),
                new Type[] { typeof(int), typeof(int) });

            // Get the IL generator for the dynamic method
            ILGenerator ilGenerator = methodBuilder.GetILGenerator();

            // Emit IL instructions to calculate the sum of two integers
            ilGenerator.Emit(OpCodes.Ldarg_0); // Load the first argument onto the stack
            ilGenerator.Emit(OpCodes.Ldarg_1); // Load the second argument onto the stack
            ilGenerator.Emit(OpCodes.Add);      // Add the two values on the stack
            ilGenerator.Emit(OpCodes.Ret);      // Return the result

            // Create the dynamic type
            Type dynamicType = typeBuilder.CreateType();

            // Invoke the dynamic method
            MethodInfo addMethod = dynamicType.GetMethod("Add");
            int result = (int)addMethod.Invoke(null, new object[] { 10, 20 });

            Console.WriteLine("Result: " + result);
        }
    }
}
