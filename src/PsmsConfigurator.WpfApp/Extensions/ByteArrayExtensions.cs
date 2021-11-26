using System;
using System.Collections.Generic;

namespace PsmsConfigurator.WpfApp.Extensions {
  public static class ByteArrayExtensions {
    public static byte[] GetRange(this byte[] data, int startIndex, int length) {
      var result = new byte[length];
      for (var i = 0; i < length; i++) 
        result[i] = data[startIndex + i];
      return result;
    }

    public static byte GetByte(this byte[] rawData, int index, int shift, byte mask) =>
      (byte)((rawData[index] >> shift) & mask);

    public static void SetByte(this byte[] rawData, byte value, int index, int shift, byte mask) =>
      rawData[index] |= (byte) ((value & mask) << shift);

    public static void SetByteRange(this IList<byte> source, IReadOnlyList<byte> value, int index, int size) {
      for(var i = 0; i < size; i++)
        source[index + i] |= value[i];
    }

    public static ushort GetUshort(this byte[] source, int index, int size = sizeof(ushort), ushort mask = ushort.MaxValue,
      int shift = 0, bool isLittleEndian = true) {
      var bytes = new byte[sizeof(ushort)];
      Array.Copy(source.GetRange(index, size),
        bytes, size);
      if(BitConverter.IsLittleEndian != isLittleEndian)
        Array.Reverse(bytes);
      return (ushort)((BitConverter.ToUInt16(bytes, 0) >> shift) & mask);
    }

    public static void SetUshort(this byte[] source, ushort value, int index, int size = sizeof(ushort),
      ushort mask = ushort.MaxValue, int shift = 0, bool isLittleEndian = true) {
      var bytes = BitConverter.GetBytes((ushort)((value & mask) << shift));
      if(BitConverter.IsLittleEndian != isLittleEndian)
        Array.Reverse(bytes);
      source.SetByteRange(bytes, index, size);
    }

    public static uint GetUint(this byte[] source, int index, int size = sizeof(uint), uint mask = uint.MaxValue, int shift = 0,
      bool isLittleEndian = true) {
      var bytes = new byte[sizeof(uint)];
      Array.Copy(source.GetRange(index, size),
        bytes, size);
      if(BitConverter.IsLittleEndian != isLittleEndian)
        Array.Reverse(bytes);
      return (BitConverter.ToUInt32(bytes, 0) >> shift) & mask;
    }

    public static void SetUint(this byte[] source, uint value, int index, int size = sizeof(uint), uint mask = uint.MaxValue, int shift = 0,
      bool isLittleEndian = true) {
      var bytes = BitConverter.GetBytes((value & mask) << shift);
      if(!isLittleEndian)
        Array.Reverse(bytes);
      source.SetByteRange(bytes, index, size);
    }

    public static ulong GetUlong(this byte[] source, int index, int size = sizeof(ulong), ulong mask = ulong.MaxValue, int shift = 0,
      bool isLittleEndian = true) {
      var bytes = new byte[sizeof(ulong)];
      Array.Copy(source.GetRange(index, size),
        bytes, size);
      if(BitConverter.IsLittleEndian != isLittleEndian)
        Array.Reverse(bytes);
      return (BitConverter.ToUInt64(bytes, 0) >> shift) & mask;
    }

    public static void SetUlong(this byte[] source, ulong value, int index, 
      int size, ulong mask = ulong.MaxValue, int shift = 0, bool isLittleEndian = true) {
      var bytes = BitConverter.GetBytes((value & mask) << shift);
      if(!isLittleEndian)
        Array.Reverse(bytes);
      source.SetByteRange(bytes, index, size);
    }
  }
}