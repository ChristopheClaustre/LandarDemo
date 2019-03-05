using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Noesis
{

    public partial class DependencyProperty
    {
        public static DependencyProperty Register(string name, Type propertyType,
            Type ownerType)
        {
            return RegisterCommon(name, propertyType, ownerType, null);
        }
        public static DependencyProperty Register(string name, Type propertyType,
            Type ownerType, PropertyMetadata typeMetadata)
        {
            return RegisterCommon(name, propertyType, ownerType, typeMetadata);
        }

        public static DependencyProperty RegisterAttached(string name, Type propertyType,
            Type ownerType)
        {
            return RegisterCommon(name, propertyType, ownerType, null);
        }

        public static DependencyProperty RegisterAttached(string name, Type propertyType,
            Type ownerType, PropertyMetadata defaultMetadata)
        {
            return RegisterCommon(name, propertyType, ownerType, defaultMetadata);
        }

        public void OverrideMetadata(Type forType, PropertyMetadata typeMetadata)
        {
            IntPtr forTypePtr = Noesis.Extend.EnsureNativeType(forType, false);

            Noesis_OverrideMetadata_(forTypePtr, swigCPtr.Handle,
                PropertyMetadata.getCPtr(typeMetadata).Handle);
        }

        internal static bool RegisterCalled { get; set; }

        #region Register implementation

        private static DependencyProperty RegisterCommon(string name, Type propertyType,
            Type ownerType, PropertyMetadata propertyMetadata)
        {
            ValidateParams(name, propertyType, ownerType);

            // Force native type registration, but skip DP registration because we are inside
            // static constructor and DP are already being registered
            IntPtr ownerTypePtr = Noesis.Extend.EnsureNativeType(ownerType, false);

            // Check property type is supported and get the registered native type
            Type originalPropertyType = propertyType;
            IntPtr nativeType = ValidatePropertyType(ref propertyType);

            // Create and register dependency property
            IntPtr dependencyPtr = Noesis_RegisterDependencyProperty_(ownerTypePtr,
                name, nativeType, PropertyMetadata.getCPtr(propertyMetadata).Handle);

            DependencyProperty dependencyProperty = new DependencyProperty(dependencyPtr, false);
            dependencyProperty.OriginalPropertyType = originalPropertyType;

            RegisterCalled = true;
            return dependencyProperty;
        }

        private static void ValidateParams(string name, Type propertyType,
            Type ownerType)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (name.Length == 0)
            {
                throw new ArgumentException("Property name can't be empty");
            }

            if (ownerType == null)
            {
                throw new ArgumentNullException("ownerType");
            }

            if (propertyType == null)
            {
                throw new ArgumentNullException("propertyType");
            }
        }

        private Type OriginalPropertyType { get; set; }

        private static IntPtr ValidatePropertyType(ref Type propertyType)
        {
            Type validType;
            if (_validTypes.TryGetValue(propertyType.TypeHandle, out validType))
            {
                propertyType = validType;
            }

            return Noesis.Extend.EnsureNativeType(propertyType);
        }

        private static Dictionary<RuntimeTypeHandle, Type> _validTypes = CreateValidTypes();

        private static Dictionary<RuntimeTypeHandle, Type> CreateValidTypes()
        {
            Dictionary<RuntimeTypeHandle, Type> validTypes =
                new Dictionary<RuntimeTypeHandle, Type>(13);

            validTypes[typeof(decimal).TypeHandle] = typeof(double);
            validTypes[typeof(long).TypeHandle] = typeof(int);
            validTypes[typeof(ulong).TypeHandle] = typeof(uint);
            validTypes[typeof(char).TypeHandle] = typeof(uint);
            validTypes[typeof(sbyte).TypeHandle] = typeof(short);
            validTypes[typeof(byte).TypeHandle] = typeof(ushort);

            validTypes[typeof(decimal?).TypeHandle] = typeof(double?);
            validTypes[typeof(long?).TypeHandle] = typeof(int?);
            validTypes[typeof(ulong?).TypeHandle] = typeof(uint?);
            validTypes[typeof(char?).TypeHandle] = typeof(uint?);
            validTypes[typeof(sbyte?).TypeHandle] = typeof(short?);
            validTypes[typeof(byte?).TypeHandle] = typeof(ushort?);

            validTypes[typeof(Type).TypeHandle] = typeof(ResourceKeyType);

            return validTypes;
        }

        #endregion

        #region Imports

        private static IntPtr Noesis_RegisterDependencyProperty_(IntPtr classType,
            string propertyName, IntPtr propertyType, IntPtr typeMetadata)
        {
            IntPtr result = Noesis_RegisterDependencyProperty(classType, propertyName, propertyType,
                typeMetadata);
            Error.Check();
            return result;
        }

        private static void Noesis_OverrideMetadata_(IntPtr classType, IntPtr dependencyProperty,
            IntPtr typeMetadata)
        {
            Noesis_OverrideMetadata(classType, dependencyProperty, typeMetadata);
            Error.Check();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport(Library.Name)]
        private static extern IntPtr Noesis_RegisterDependencyProperty(IntPtr classType,
            [MarshalAs(UnmanagedType.LPStr)]string propertyName,
            IntPtr propertyType, IntPtr propertyMetadata);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport(Library.Name)]
        private static extern void Noesis_OverrideMetadata(IntPtr classType,
            IntPtr dependencyProperty, IntPtr propertyMetadata);

        #endregion
    }

}
