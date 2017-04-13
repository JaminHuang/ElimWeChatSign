using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace Titan.EmitObjectAccessor
{
    public class PropertyAccessor : IPropertyAccessor
    {
        private static readonly Dictionary<Type, OpCode> typeOpCodes = new Dictionary<Type, OpCode>()
            {
			    {typeof(sbyte),OpCodes.Ldind_I1},
			    {typeof(byte),OpCodes.Ldind_U1},
			    {typeof(char),OpCodes.Ldind_U2},
			    {typeof(short),OpCodes.Ldind_I2},
			    {typeof(ushort),OpCodes.Ldind_U2},
			    {typeof(int),OpCodes.Ldind_I4},
			    {typeof(uint),OpCodes.Ldind_U4},
			    {typeof(long),OpCodes.Ldind_I8},
			    {typeof(ulong),OpCodes.Ldind_I8},
			    {typeof(bool),OpCodes.Ldind_I1},
			    {typeof(double),OpCodes.Ldind_R8},
                {typeof(float),OpCodes.Ldind_R4} 
            };
        //private static readonly Dictionary<PropertyKey, PropertyAccessor2> cachedAccessors = new Dictionary<PropertyKey, PropertyAccessor2>();
        //public Type Type { get; private set; }
        //public string PropertyName { get; private set; }
        public PropertyInfo PropertyInfo { get; set; }

        //public PropertyAccessor(Type type, string propertyName)
        //{
        //    this.Type = type;
        //    this.PropertyName = propertyName;
        //}
        public PropertyAccessor(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
        }
        public object Get(object target)
        {
            if (emittedPropertyAccessor == null)
            {
                CreateEmittedPropertyAccessor();
            }
            return emittedPropertyAccessor.Get(target);
        }

        public void Set(object target, object value)
        {
            if (emittedPropertyAccessor == null)
            {
                CreateEmittedPropertyAccessor();
            }
            emittedPropertyAccessor.Set(target, value);
        }

        private IPropertyAccessor emittedPropertyAccessor;
        private void CreateEmittedPropertyAccessor()
        {
            Assembly assembly = EmitAssembly();

            emittedPropertyAccessor = assembly.CreateInstance("Property") as IPropertyAccessor;
        }


        /// <summary>
        /// Create an assembly that will provide the get and set methods.
        /// </summary>
        private Assembly EmitAssembly()
        {
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "PropertyAccessorAssembly";
            AssemblyBuilder newAssembly = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder newModule = newAssembly.DefineDynamicModule("Module");
            TypeBuilder myType = newModule.DefineType("Property", TypeAttributes.Public);
            myType.AddInterfaceImplementation(typeof(IPropertyAccessor));
            ConstructorBuilder constructor = myType.DefineDefaultConstructor(MethodAttributes.Public);

            #region Get
            Type[] getParamTypes = { typeof(object) };
            Type getReturnType = typeof(object);
            MethodBuilder getMethod =
                myType.DefineMethod("Get",
                MethodAttributes.Public | MethodAttributes.Virtual,
                getReturnType,
                getParamTypes);

            //
            // From the method, get an ILGenerator. This is used to
            // emit the IL that we want.
            //
            ILGenerator getIL = getMethod.GetILGenerator();

            //
            // Emit the IL. 
            //
            //MethodInfo targetGetMethod = this.Type.GetMethod("get_" + this.PropertyName);
            MethodInfo targetGetMethod = PropertyInfo.GetGetMethod();
            if (targetGetMethod != null)
            {
                getIL.DeclareLocal(typeof(object));
                getIL.Emit(OpCodes.Ldarg_1);								//Load the first argument 
                //(target object) 
                getIL.Emit(OpCodes.Castclass, PropertyInfo.DeclaringType);			//Cast to the source type 
                getIL.EmitCall(OpCodes.Call, targetGetMethod, null);		//Get the property value 
                if (targetGetMethod.ReturnType.IsValueType)
                {
                    getIL.Emit(OpCodes.Box, targetGetMethod.ReturnType);	//Box if necessary
                }
                getIL.Emit(OpCodes.Stloc_0);								//Store it 
                getIL.Emit(OpCodes.Ldloc_0);
            }
            else
            {
                getIL.ThrowException(typeof(MissingMethodException));
            }
            getIL.Emit(OpCodes.Ret);
            #endregion

            #region Set
            Type[] setParamTypes = new Type[] { typeof(object), typeof(object) };
            Type setReturnType = null;
            MethodBuilder setMethod =
                myType.DefineMethod("Set",
                MethodAttributes.Public | MethodAttributes.Virtual,
                setReturnType,
                setParamTypes);

            //
            // From the method, get an ILGenerator. This is used to
            // emit the IL that we want.
            //
            ILGenerator setIL = setMethod.GetILGenerator();
            //
            // Emit the IL. 
            // 
            //MethodInfo targetSetMethod = this.Type.GetMethod("set_" + this.PropertyName);
            MethodInfo targetSetMethod = PropertyInfo.GetSetMethod();
            if (targetSetMethod != null)
            {
                Type paramType = targetSetMethod.GetParameters()[0].ParameterType;
                setIL.DeclareLocal(paramType);
                setIL.Emit(OpCodes.Ldarg_1);						//Load the first argument 
                //(target object) 
                setIL.Emit(OpCodes.Castclass, PropertyInfo.DeclaringType);	//Cast to the source type 
                setIL.Emit(OpCodes.Ldarg_2);						//Load the second argument 
                //(value object) 
                if (paramType.IsValueType)
                {
                    setIL.Emit(OpCodes.Unbox, paramType);			//Unbox it 	
                    if (typeOpCodes.ContainsKey(paramType))					//and load
                    {
                        OpCode load = typeOpCodes[paramType];
                        setIL.Emit(load);
                    }
                    else
                    {
                        setIL.Emit(OpCodes.Ldobj, paramType);
                    }
                }
                else
                {
                    setIL.Emit(OpCodes.Castclass, paramType); //Cast class
                }
                setIL.EmitCall(OpCodes.Callvirt, targetSetMethod, null); //Set the property value
            }
            else
            {
                setIL.ThrowException(typeof(MissingMethodException));
            }

            setIL.Emit(OpCodes.Ret);
            #endregion

            //
            // Load the type
            //
            myType.CreateType();

            return newAssembly;
        }
    }
}
