using System;
using System.Runtime.InteropServices;

namespace Playbook_Editor
{
    public enum TdbFieldType { tdbString = 0, tdbBinary = 1, tdbSInt = 2, tdbUInt = 3, tdbFloat = 4, tdbVarchar = 0xD, tdbLongVarchar = 0xE, tdbInt = 0x2CE };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TdbFieldProperties
    {
        public String Name;
        public int Size;
        public TdbFieldType FieldType;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    struct TdbTableProperties
    {
        public String Name;
        public int FieldCount;
        public int Capacity;
        public int RecordCount;
        public int DeletedCount;
        public int NextDeletedRecord;
        public bool Flag0;
        public bool Flag1;
        public bool Flag2;
        public bool Flag3;
        public bool NonAllocated;
        public bool HasVarChar;
        public bool HasCompressedVarChar;
    }

    class TDB
    {
        private const string TDBACCESS_DLL = "tdbaccess.dll";

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern int TDBOpen(string FileName);

        [DllImport(TDBACCESS_DLL)]
        public static extern bool TDBClose(int DBIndex);

        [DllImport(TDBACCESS_DLL)]
        public static extern bool TDBSave(int DBIndex);

        [DllImport(TDBACCESS_DLL)]
        public static extern bool TDBDatabaseCompact(int DBIndex);

        [DllImport(TDBACCESS_DLL)]
        public static extern int TDBDatabaseGetTableCount(int DBIndex);

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern bool TDBFieldGetProperties(int DBIndex, string TableName, int FieldIndex, ref TdbFieldProperties FieldProperties);

        [DllImport(TDBACCESS_DLL)]
        public static extern bool TDBTableGetProperties(int DBIndex, int TableIndex, ref TdbTableProperties TableProperties);

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern bool TDBFieldGetValueAsBinary(int DBIndex, string TableName, string FieldName, int RecNo, ref string OutBuffer);

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern float TDBFieldGetValueAsFloat(int DBIndex, string TableName, string FieldName, int RecNo);

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern int TDBFieldGetValueAsInteger(int DBIndex, string TableName, string FieldName, int RecNo);

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern bool TDBFieldGetValueAsString(int DBIndex, string TableName, string FieldName, int RecNo, ref string OutBuffer);

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern bool TDBFieldSetValueAsFloat(int DBIndex, string TableName, string FieldName, int RecNo, float NewValue);

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern bool TDBFieldSetValueAsInteger(int DBIndex, string TableName, string FieldName, int RecNo, int NewValue);

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern bool TDBFieldSetValueAsString(int DBIndex, string TableName, string FieldName, int RecNo, string NewValue);

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern int TDBQueryFindUnsignedInt(int DBIndex, string TableName, string FieldName, int Value);

        [DllImport(TDBACCESS_DLL)]
        public static extern int TDBQueryGetResult(int Index);

        [DllImport(TDBACCESS_DLL)]
        public static extern int TDBQueryGetResultSize();

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern int TDBTableRecordAdd(int DBIndex, string TableName, bool AllowExpand);

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern bool TDBTableRecordChangeDeleted(int DBIndex, string TableName, int RecNo, bool Deleted);

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern bool TDBTableRecordDeleted(int DBIndex, string TableName, int RecNo);

        [DllImport(TDBACCESS_DLL, CharSet = CharSet.Unicode)]
        public static extern bool TDBTableRecordRemove(int DBIndex, string TableName, int RecNo);
    }
}
