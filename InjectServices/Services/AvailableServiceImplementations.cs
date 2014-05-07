using InjectServices.Clients;
using InjectServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace InjectServices
{
    public class AvailableServiceImplementations
    {
       // public ServiceResolver ServiceResolver { get; set; }

        public AvailableServiceImplementations()
        {
        }

        internal void Init(ServiceResolver ServiceResolver)
        {
            var assembly = Assembly.GetExecutingAssembly();
            //emit staff

            //-----------------
            //get all classes
            Type[] info = assembly.GetTypes();

            //foreach class in assembly
            foreach (Type currClass in info)
            {
                //get all proparties
                PropertyInfo[] propartiesInClass = currClass.GetProperties();

                //foreach propartie
                foreach (PropertyInfo p in propartiesInClass)
                {
                    //get atribute of type InjectionAttribute
                    var attr = p.GetCustomAttributes(typeof(InjectServices.InjectionAttribute));

                    //if such exists
                    if (attr.Count() == 1)
                    {
                        //get the type that must be inst
                         Type typeOfPropartie = p.PropertyType;
                        
                        // find it in ServiceDeclaration
                        foreach (KeyValuePair<Type, object> service in ServiceResolver.Services)
                        {
                            bool interfacesMatch = service.Key == typeOfPropartie;

                            if (interfacesMatch == true)
                            {
                                //get the created instance

                                // p.SetValueObjectExtension(service.Value, );
                                Type t = service.Value as Type;
                                var instance = Activator.CreateInstance(t, true);
                               //var method = p.GetGetMethod();
                               //method = OverrideGetMethodOfPropatie;
                                this.OverrideGetMethodOfPropatie(p, assembly, currClass, instance);
                                //and set it as default value of the prop 
                                //MethodInfo methodInfo = p.GetGetMethod();
                                // methodInfo.
                               // p.SetValue(
                               
                            }
                        }

                       // var instance = Activator.CreateInstance(typeof(TaxCalculator));
                       // MethodInfo getMethodInfo = p.GetGetMethod(true);
                       // MethodBody body = getMethodInfo.GetMethodBody();
                       // p.SetValue(p.Name, instance);
                    }
                }
            }
        }

        
        private void OverrideGetMethodOfPropatie(PropertyInfo propertyInfo, Assembly assembly, Type type, object instance)
        {
            //type --> class
            AssemblyName aName = assembly.GetName();
            AppDomain currentDomain = AppDomain.CurrentDomain;
            AssemblyBuilder builder = currentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);

            ModuleBuilder module = builder.DefineDynamicModule("Proxies");

            TypeBuilder proxy = module.DefineType(type.Name + "Proxy", 
                TypeAttributes.Public | 
                TypeAttributes.AutoClass | 
                TypeAttributes.AnsiClass | 
                TypeAttributes.BeforeFieldInit, 
                type);


           // ConstructorBuilder constructor = proxy.DefineConstructo(MethodAttributes.Public, CallingConventions.Standard, new Type[0]);
            PropertyBuilder property = proxy.DefineProperty(propertyInfo.Name, propertyInfo.Attributes, propertyInfo.PropertyType, null);


            MethodAttributes attributes = 
                MethodAttributes.Public | 
                MethodAttributes.HideBySig | 
                MethodAttributes.SpecialName | 
                MethodAttributes.Virtual;

            Type[] tparams = { };

            MethodBuilder getMethod = this.GetMethod(proxy, "get_" + propertyInfo.Name, typeof(TaxCalculator), tparams);

            ILGenerator getIL = getMethod.GetILGenerator();

            LocalBuilder instanceLocalVariable = getIL.DeclareLocal(typeof(TaxCalculator)); //empty
            getIL.Emit(OpCodes.Newobj, typeof(TaxCalculator).GetConstructor(new Type[0])); // store the actual instance ...
            getIL.Emit(OpCodes.Stloc, instanceLocalVariable); //... into instanceLocalVariable
            getIL.Emit(OpCodes.Ldloc, instanceLocalVariable); //load instanceLocalVariable
            //getIL.Emit(OpCodes.Ldarg_1);
            //getIL.Emit(OpCodes.Ldarg_2);
            //generator.Emit(OpCodes.Add_Ovf);
            //getIL.Emit(OpCodes.Conv_R4);
           // getIL.Emit(OpCodes.Stloc_0);


            getIL.Emit(OpCodes.Ret);   

            property.SetGetMethod(getMethod);
        }

        public MethodBuilder GetMethod(TypeBuilder typBuilder, string methodName,
                      Type returnType, params Type[] parameterTypes)
        {
            MethodBuilder builder = typBuilder.DefineMethod(methodName,
                              MethodAttributes.Public | MethodAttributes.HideBySig,
                                     CallingConventions.HasThis, returnType, parameterTypes);
            return builder;
        }

       
    }

    public static class Extensions
    {
        public static void InitProp(this object self, object instance, Type type)
        {
            TaxCalculator t = new TaxCalculator();
           var ins = self as TaxCalculator;
            ins = t;
        }
    }
}
