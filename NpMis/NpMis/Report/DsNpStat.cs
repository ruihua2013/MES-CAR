namespace NpMis.Report
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable, DesignerCategory("code"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedDataSetSchema"), HelpKeyword("vs.data.DataSet"), ToolboxItem(true), XmlRoot("DsNpStat")]
    public class DsNpStat : DataSet
    {
        private System.Data.SchemaSerializationMode _schemaSerializationMode;
        private V_NpStatDataTable tableV_NpStat;

        [DebuggerNonUserCode]
        public DsNpStat()
        {
            this._schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            base.BeginInit();
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
            base.EndInit();
        }

        [DebuggerNonUserCode]
        protected DsNpStat(SerializationInfo info, StreamingContext context) : base(info, context, false)
        {
            this._schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            if (base.IsBinarySerialized(info, context))
            {
                this.InitVars(false);
                CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += handler;
                this.Relations.CollectionChanged += handler;
            }
            else
            {
                string s = (string) info.GetValue("XmlSchema", typeof(string));
                if (base.DetermineSchemaSerializationMode(info, context) == System.Data.SchemaSerializationMode.IncludeSchema)
                {
                    DataSet dataSet = new DataSet();
                    dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                    if (dataSet.Tables["V_NpStat"] != null)
                    {
                        base.Tables.Add(new V_NpStatDataTable(dataSet.Tables["V_NpStat"]));
                    }
                    base.DataSetName = dataSet.DataSetName;
                    base.Prefix = dataSet.Prefix;
                    base.Namespace = dataSet.Namespace;
                    base.Locale = dataSet.Locale;
                    base.CaseSensitive = dataSet.CaseSensitive;
                    base.EnforceConstraints = dataSet.EnforceConstraints;
                    base.Merge(dataSet, false, MissingSchemaAction.Add);
                    this.InitVars();
                }
                else
                {
                    base.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                }
                base.GetSerializationData(info, context);
                CollectionChangeEventHandler handler2 = new CollectionChangeEventHandler(this.SchemaChanged);
                base.Tables.CollectionChanged += handler2;
                this.Relations.CollectionChanged += handler2;
            }
        }

        [DebuggerNonUserCode]
        public override DataSet Clone()
        {
            DsNpStat stat = (DsNpStat) base.Clone();
            stat.InitVars();
            stat.SchemaSerializationMode = this.SchemaSerializationMode;
            return stat;
        }

        [DebuggerNonUserCode]
        protected override XmlSchema GetSchemaSerializable()
        {
            MemoryStream w = new MemoryStream();
            base.WriteXmlSchema(new XmlTextWriter(w, null));
            w.Position = 0L;
            return XmlSchema.Read(new XmlTextReader(w), null);
        }

        [DebuggerNonUserCode]
        public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
        {
            DsNpStat stat = new DsNpStat();
            XmlSchemaComplexType type = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            xs.Add(stat.GetSchemaSerializable());
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = stat.Namespace
            };
            sequence.Items.Add(item);
            type.Particle = sequence;
            return type;
        }

        [DebuggerNonUserCode]
        private void InitClass()
        {
            base.DataSetName = "DsNpStat";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/DsNpStat.xsd";
            base.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableV_NpStat = new V_NpStatDataTable();
            base.Tables.Add(this.tableV_NpStat);
        }

        [DebuggerNonUserCode]
        protected override void InitializeDerivedDataSet()
        {
            base.BeginInit();
            this.InitClass();
            base.EndInit();
        }

        [DebuggerNonUserCode]
        internal void InitVars()
        {
            this.InitVars(true);
        }

        [DebuggerNonUserCode]
        internal void InitVars(bool initTable)
        {
            this.tableV_NpStat = (V_NpStatDataTable) base.Tables["V_NpStat"];
            if (initTable && (this.tableV_NpStat != null))
            {
                this.tableV_NpStat.InitVars();
            }
        }

        [DebuggerNonUserCode]
        protected override void ReadXmlSerializable(XmlReader reader)
        {
            if (base.DetermineSchemaSerializationMode(reader) == System.Data.SchemaSerializationMode.IncludeSchema)
            {
                this.Reset();
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(reader);
                if (dataSet.Tables["V_NpStat"] != null)
                {
                    base.Tables.Add(new V_NpStatDataTable(dataSet.Tables["V_NpStat"]));
                }
                base.DataSetName = dataSet.DataSetName;
                base.Prefix = dataSet.Prefix;
                base.Namespace = dataSet.Namespace;
                base.Locale = dataSet.Locale;
                base.CaseSensitive = dataSet.CaseSensitive;
                base.EnforceConstraints = dataSet.EnforceConstraints;
                base.Merge(dataSet, false, MissingSchemaAction.Add);
                this.InitVars();
            }
            else
            {
                base.ReadXml(reader);
                this.InitVars();
            }
        }

        [DebuggerNonUserCode]
        private void SchemaChanged(object sender, CollectionChangeEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Remove)
            {
                this.InitVars();
            }
        }

        [DebuggerNonUserCode]
        protected override bool ShouldSerializeRelations()
        {
            return false;
        }

        [DebuggerNonUserCode]
        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializeV_NpStat()
        {
            return false;
        }

        [DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataRelationCollection Relations
        {
            get
            {
                return base.Relations;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DebuggerNonUserCode]
        public override System.Data.SchemaSerializationMode SchemaSerializationMode
        {
            get
            {
                return this._schemaSerializationMode;
            }
            set
            {
                this._schemaSerializationMode = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DebuggerNonUserCode]
        public DataTableCollection Tables
        {
            get
            {
                return base.Tables;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DebuggerNonUserCode, Browsable(false)]
        public V_NpStatDataTable V_NpStat
        {
            get
            {
                return this.tableV_NpStat;
            }
        }

        [Serializable, XmlSchemaProvider("GetTypedTableSchema"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class V_NpStatDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDescription;
            private DataColumn columnTotalNum;

            public event DsNpStat.V_NpStatRowChangeEventHandler V_NpStatRowChanged;

            public event DsNpStat.V_NpStatRowChangeEventHandler V_NpStatRowChanging;

            public event DsNpStat.V_NpStatRowChangeEventHandler V_NpStatRowDeleted;

            public event DsNpStat.V_NpStatRowChangeEventHandler V_NpStatRowDeleting;

            [DebuggerNonUserCode]
            public V_NpStatDataTable()
            {
                base.TableName = "V_NpStat";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal V_NpStatDataTable(DataTable table)
            {
                base.TableName = table.TableName;
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
            }

            [DebuggerNonUserCode]
            protected V_NpStatDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddV_NpStatRow(DsNpStat.V_NpStatRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public DsNpStat.V_NpStatRow AddV_NpStatRow(string Description, int TotalNum)
            {
                DsNpStat.V_NpStatRow row = (DsNpStat.V_NpStatRow) base.NewRow();
                row.ItemArray = new object[] { Description, TotalNum };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                DsNpStat.V_NpStatDataTable table = (DsNpStat.V_NpStatDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new DsNpStat.V_NpStatDataTable();
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(DsNpStat.V_NpStatRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                DsNpStat stat = new DsNpStat();
                xs.Add(stat.GetSchemaSerializable());
                XmlSchemaAny item = new XmlSchemaAny {
                    Namespace = "http://www.w3.org/2001/XMLSchema",
                    MinOccurs = 0M,
                    MaxOccurs = 79228162514264337593543950335M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(item);
                XmlSchemaAny any2 = new XmlSchemaAny {
                    Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                    MinOccurs = 1M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(any2);
                XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                    Name = "namespace",
                    FixedValue = stat.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "V_NpStatDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }

            [DebuggerNonUserCode]
            private void InitClass()
            {
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                this.columnTotalNum = new DataColumn("TotalNum", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnTotalNum);
                this.columnDescription.MaxLength = 200;
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columnDescription = base.Columns["Description"];
                this.columnTotalNum = base.Columns["TotalNum"];
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new DsNpStat.V_NpStatRow(builder);
            }

            [DebuggerNonUserCode]
            public DsNpStat.V_NpStatRow NewV_NpStatRow()
            {
                return (DsNpStat.V_NpStatRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.V_NpStatRowChanged != null)
                {
                    this.V_NpStatRowChanged(this, new DsNpStat.V_NpStatRowChangeEvent((DsNpStat.V_NpStatRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.V_NpStatRowChanging != null)
                {
                    this.V_NpStatRowChanging(this, new DsNpStat.V_NpStatRowChangeEvent((DsNpStat.V_NpStatRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.V_NpStatRowDeleted != null)
                {
                    this.V_NpStatRowDeleted(this, new DsNpStat.V_NpStatRowChangeEvent((DsNpStat.V_NpStatRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.V_NpStatRowDeleting != null)
                {
                    this.V_NpStatRowDeleting(this, new DsNpStat.V_NpStatRowChangeEvent((DsNpStat.V_NpStatRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemoveV_NpStatRow(DsNpStat.V_NpStatRow row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode, Browsable(false)]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn DescriptionColumn
            {
                get
                {
                    return this.columnDescription;
                }
            }

            [DebuggerNonUserCode]
            public DsNpStat.V_NpStatRow this[int index]
            {
                get
                {
                    return (DsNpStat.V_NpStatRow) base.Rows[index];
                }
            }

            [DebuggerNonUserCode]
            public DataColumn TotalNumColumn
            {
                get
                {
                    return this.columnTotalNum;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class V_NpStatRow : DataRow
        {
            private DsNpStat.V_NpStatDataTable tableV_NpStat;

            [DebuggerNonUserCode]
            internal V_NpStatRow(DataRowBuilder rb) : base(rb)
            {
                this.tableV_NpStat = (DsNpStat.V_NpStatDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableV_NpStat.DescriptionColumn);
            }

            [DebuggerNonUserCode]
            public bool IsTotalNumNull()
            {
                return base.IsNull(this.tableV_NpStat.TotalNumColumn);
            }

            [DebuggerNonUserCode]
            public void SetDescriptionNull()
            {
                base[this.tableV_NpStat.DescriptionColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetTotalNumNull()
            {
                base[this.tableV_NpStat.TotalNumColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_NpStat.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_NpStat”中列“Description”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_NpStat.DescriptionColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public int TotalNum
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableV_NpStat.TotalNumColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_NpStat”中列“TotalNum”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableV_NpStat.TotalNumColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class V_NpStatRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private DsNpStat.V_NpStatRow eventRow;

            [DebuggerNonUserCode]
            public V_NpStatRowChangeEvent(DsNpStat.V_NpStatRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode]
            public DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }

            [DebuggerNonUserCode]
            public DsNpStat.V_NpStatRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void V_NpStatRowChangeEventHandler(object sender, DsNpStat.V_NpStatRowChangeEvent e);
    }
}

