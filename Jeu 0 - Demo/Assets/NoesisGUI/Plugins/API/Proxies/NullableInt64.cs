//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.10
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;

namespace Noesis
{

[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
internal struct NullableInt64 {

  [MarshalAs(UnmanagedType.U1)]
  private bool _hasValue;
  [MarshalAs(UnmanagedType.I8)]
  private long _value;

  public bool HasValue { get { return this._hasValue; } }

  public long Value {
    get {
      if (!HasValue) {
        throw new InvalidOperationException("Nullable does not have a value");
      }
      return this._value;
    }
  }

  public NullableInt64(long v) {
    this._hasValue = true;
    this._value = v;
  }

  public static explicit operator long(NullableInt64 n) {
    if (!n.HasValue) {
      throw new InvalidOperationException("Nullable does not have a value");
    }
    return n.Value;
  }

  public static implicit operator NullableInt64(long v) {
    return new NullableInt64(v);
  }

  public static implicit operator Nullable<long>(NullableInt64 n) {
    return n.HasValue ? new Nullable<long>(n.Value) : new Nullable<long>();
  }

  public static implicit operator NullableInt64(Nullable<long> n) {
    return n.HasValue ? new NullableInt64(n.Value) : new NullableInt64();
  }

  public static bool operator==(NullableInt64 n, long v) {
    return n.HasValue && n.Value == v;
  }

  public static bool operator!=(NullableInt64 n, long v) {
    return !(n == v);
  }

  public static bool operator==(long v, NullableInt64 n) {
    return n == v;
  }
  
  public static bool operator!=(long v, NullableInt64 n) {
    return n != v;
  }

  public static bool operator==(NullableInt64 n0, NullableInt64 n1) {
    return n0.HasValue && n1.HasValue ? n0.Value == n1.Value : n0.HasValue == n1.HasValue;
  }

  public static bool operator!=(NullableInt64 n0, NullableInt64 n1) {
    return !(n0 == n1);
  }

  public override bool Equals(System.Object obj) {
    return obj is NullableInt64 && this == (NullableInt64)obj;
  }

  public bool Equals(NullableInt64 n) {
    return this == n;
  }

  public override int GetHashCode() {
    return HasValue ? Value.GetHashCode() : 0;
  }

}

}
