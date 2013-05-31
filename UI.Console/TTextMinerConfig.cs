﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: TTextMinerConfig.proto
namespace SemanticDataEnrichment.UI.TestConsole
{
	[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"TTextMinerConfig")]
	public partial class TTextMinerConfig : global::ProtoBuf.IExtensible
	{
		public TTextMinerConfig() { }

		private string _Dictionary;
		[global::ProtoBuf.ProtoMember(1, IsRequired = true, Name = @"Dictionary", DataFormat = global::ProtoBuf.DataFormat.Default)]
		public string Dictionary
		{
			get { return _Dictionary; }
			set { _Dictionary = value; }
		}

		private string _PrettyOutput = "";
		[global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"PrettyOutput", DataFormat = global::ProtoBuf.DataFormat.Default)]
		[global::System.ComponentModel.DefaultValue("")]
		public string PrettyOutput
		{
			get { return _PrettyOutput; }
			set { _PrettyOutput = value; }
		}

		private uint _NumThreads = (uint)1;
		[global::ProtoBuf.ProtoMember(3, IsRequired = false, Name = @"NumThreads", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
		[global::System.ComponentModel.DefaultValue((uint)1)]
		public uint NumThreads
		{
			get { return _NumThreads; }
			set { _NumThreads = value; }
		}

		private string _Language = @"ru";
		[global::ProtoBuf.ProtoMember(4, IsRequired = false, Name = @"Language", DataFormat = global::ProtoBuf.DataFormat.Default)]
		[global::System.ComponentModel.DefaultValue(@"ru")]
		public string Language
		{
			get { return _Language; }
			set { _Language = value; }
		}

		private string _PrintTree = "";
		[global::ProtoBuf.ProtoMember(5, IsRequired = false, Name = @"PrintTree", DataFormat = global::ProtoBuf.DataFormat.Default)]
		[global::System.ComponentModel.DefaultValue("")]
		public string PrintTree
		{
			get { return _PrintTree; }
			set { _PrintTree = value; }
		}

		private string _PrintRules = "";
		[global::ProtoBuf.ProtoMember(6, IsRequired = false, Name = @"PrintRules", DataFormat = global::ProtoBuf.DataFormat.Default)]
		[global::System.ComponentModel.DefaultValue("")]
		public string PrintRules
		{
			get { return _PrintRules; }
			set { _PrintRules = value; }
		}

		private TTextMinerConfig.TInputParams _Input = null;
		[global::ProtoBuf.ProtoMember(7, IsRequired = false, Name = @"Input", DataFormat = global::ProtoBuf.DataFormat.Default)]
		[global::System.ComponentModel.DefaultValue(null)]
		public TTextMinerConfig.TInputParams Input
		{
			get { return _Input; }
			set { _Input = value; }
		}

		private TTextMinerConfig.TOutputParams _Output = null;
		[global::ProtoBuf.ProtoMember(8, IsRequired = false, Name = @"Output", DataFormat = global::ProtoBuf.DataFormat.Default)]
		[global::System.ComponentModel.DefaultValue(null)]
		public TTextMinerConfig.TOutputParams Output
		{
			get { return _Output; }
			set { _Output = value; }
		}
		private readonly global::System.Collections.Generic.List<TTextMinerConfig.TArticleRef> _Articles = new global::System.Collections.Generic.List<TTextMinerConfig.TArticleRef>();
		[global::ProtoBuf.ProtoMember(9, Name = @"Articles", DataFormat = global::ProtoBuf.DataFormat.Default)]
		public global::System.Collections.Generic.List<TTextMinerConfig.TArticleRef> Articles
		{
			get { return _Articles; }
		}

		private readonly global::System.Collections.Generic.List<TTextMinerConfig.TFactTypeRef> _Facts = new global::System.Collections.Generic.List<TTextMinerConfig.TFactTypeRef>();
		[global::ProtoBuf.ProtoMember(10, Name = @"Facts", DataFormat = global::ProtoBuf.DataFormat.Default)]
		public global::System.Collections.Generic.List<TTextMinerConfig.TFactTypeRef> Facts
		{
			get { return _Facts; }
		}


		private bool _SavePartialFacts = (bool)true;
		[global::ProtoBuf.ProtoMember(11, IsRequired = false, Name = @"SavePartialFacts", DataFormat = global::ProtoBuf.DataFormat.Default)]
		[global::System.ComponentModel.DefaultValue((bool)true)]
		public bool SavePartialFacts
		{
			get { return _SavePartialFacts; }
			set { _SavePartialFacts = value; }
		}
		private readonly global::System.Collections.Generic.List<TTextMinerConfig.TArticleRef> _ArticlesBeforeFragmentation = new global::System.Collections.Generic.List<TTextMinerConfig.TArticleRef>();
		[global::ProtoBuf.ProtoMember(12, Name = @"ArticlesBeforeFragmentation", DataFormat = global::ProtoBuf.DataFormat.Default)]
		public global::System.Collections.Generic.List<TTextMinerConfig.TArticleRef> ArticlesBeforeFragmentation
		{
			get { return _ArticlesBeforeFragmentation; }
		}

		[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"TInputParams")]
		public partial class TInputParams : global::ProtoBuf.IExtensible
		{
			public TInputParams() { }


			private string _File = "";
			[global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"File", DataFormat = global::ProtoBuf.DataFormat.Default)]
			[global::System.ComponentModel.DefaultValue("")]
			public string File
			{
				get { return _File; }
				set { _File = value; }
			}

			private string _Dir = "";
			[global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"Dir", DataFormat = global::ProtoBuf.DataFormat.Default)]
			[global::System.ComponentModel.DefaultValue("")]
			public string Dir
			{
				get { return _Dir; }
				set { _Dir = value; }
			}

			private TTextMinerConfig.TInputParams.SourceFormat _Format = TTextMinerConfig.TInputParams.SourceFormat.plain;
			[global::ProtoBuf.ProtoMember(3, IsRequired = false, Name = @"Format", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
			[global::System.ComponentModel.DefaultValue(TTextMinerConfig.TInputParams.SourceFormat.plain)]
			public TTextMinerConfig.TInputParams.SourceFormat Format
			{
				get { return _Format; }
				set { _Format = value; }
			}

			private TTextMinerConfig.TInputParams.SourceType _Type = TTextMinerConfig.TInputParams.SourceType.no;
			[global::ProtoBuf.ProtoMember(4, IsRequired = false, Name = @"Type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
			[global::System.ComponentModel.DefaultValue(TTextMinerConfig.TInputParams.SourceType.no)]
			public TTextMinerConfig.TInputParams.SourceType Type
			{
				get { return _Type; }
				set { _Type = value; }
			}

			private string _Encoding = @"utf8";
			[global::ProtoBuf.ProtoMember(5, IsRequired = false, Name = @"Encoding", DataFormat = global::ProtoBuf.DataFormat.Default)]
			[global::System.ComponentModel.DefaultValue(@"utf8")]
			public string Encoding
			{
				get { return _Encoding; }
				set { _Encoding = value; }
			}

			private string _SubKey = "";
			[global::ProtoBuf.ProtoMember(6, IsRequired = false, Name = @"SubKey", DataFormat = global::ProtoBuf.DataFormat.Default)]
			[global::System.ComponentModel.DefaultValue("")]
			public string SubKey
			{
				get { return _SubKey; }
				set { _SubKey = value; }
			}

			private int _FirstDocNum = (int)-1;
			[global::ProtoBuf.ProtoMember(7, IsRequired = false, Name = @"FirstDocNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
			[global::System.ComponentModel.DefaultValue((int)-1)]
			public int FirstDocNum
			{
				get { return _FirstDocNum; }
				set { _FirstDocNum = value; }
			}

			private int _LastDocDum = (int)-1;
			[global::ProtoBuf.ProtoMember(8, IsRequired = false, Name = @"LastDocDum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
			[global::System.ComponentModel.DefaultValue((int)-1)]
			public int LastDocDum
			{
				get { return _LastDocDum; }
				set { _LastDocDum = value; }
			}

			private string _Url2Fio = "";
			[global::ProtoBuf.ProtoMember(9, IsRequired = false, Name = @"Url2Fio", DataFormat = global::ProtoBuf.DataFormat.Default)]
			[global::System.ComponentModel.DefaultValue("")]
			public string Url2Fio
			{
				get { return _Url2Fio; }
				set { _Url2Fio = value; }
			}

			private string _Date = "";
			[global::ProtoBuf.ProtoMember(10, IsRequired = false, Name = @"Date", DataFormat = global::ProtoBuf.DataFormat.Default)]
			[global::System.ComponentModel.DefaultValue("")]
			public string Date
			{
				get { return _Date; }
				set { _Date = value; }
			}
			[global::ProtoBuf.ProtoContract(Name = @"SourceFormat")]
			public enum SourceFormat
			{

				[global::ProtoBuf.ProtoEnum(Name = @"plain", Value = 0)]
				plain = 0,

				[global::ProtoBuf.ProtoEnum(Name = @"html", Value = 1)]
				html = 1
			}

			[global::ProtoBuf.ProtoContract(Name = @"SourceType")]
			public enum SourceType
			{

				[global::ProtoBuf.ProtoEnum(Name = @"no", Value = 0)]
				no = 0,

				[global::ProtoBuf.ProtoEnum(Name = @"dpl", Value = 1)]
				dpl = 1,

				[global::ProtoBuf.ProtoEnum(Name = @"mapreduce", Value = 2)]
				mapreduce = 2,

				[global::ProtoBuf.ProtoEnum(Name = @"arcview", Value = 3)]
				arcview = 3,

				[global::ProtoBuf.ProtoEnum(Name = @"tar", Value = 4)]
				tar = 4,

				[global::ProtoBuf.ProtoEnum(Name = @"som", Value = 5)]
				som = 5,

				[global::ProtoBuf.ProtoEnum(Name = @"yarchive", Value = 6)]
				yarchive = 6
			}

			private global::ProtoBuf.IExtension extensionObject;
			global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
			{ return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
		}

		[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"TOutputParams")]
		public partial class TOutputParams : global::ProtoBuf.IExtensible
		{
			public TOutputParams() { }


			private string _File = "";
			[global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"File", DataFormat = global::ProtoBuf.DataFormat.Default)]
			[global::System.ComponentModel.DefaultValue("")]
			public string File
			{
				get { return _File; }
				set { _File = value; }
			}

			private TTextMinerConfig.TOutputParams.OutputMode _Mode = TTextMinerConfig.TOutputParams.OutputMode.append;
			[global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"Mode", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
			[global::System.ComponentModel.DefaultValue(TTextMinerConfig.TOutputParams.OutputMode.append)]
			public TTextMinerConfig.TOutputParams.OutputMode Mode
			{
				get { return _Mode; }
				set { _Mode = value; }
			}

			private TTextMinerConfig.TOutputParams.OutputFormat _Format = TTextMinerConfig.TOutputParams.OutputFormat.xml;
			[global::ProtoBuf.ProtoMember(3, IsRequired = false, Name = @"Format", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
			[global::System.ComponentModel.DefaultValue(TTextMinerConfig.TOutputParams.OutputFormat.xml)]
			public TTextMinerConfig.TOutputParams.OutputFormat Format
			{
				get { return _Format; }
				set { _Format = value; }
			}

			private string _Encoding = @"utf8";
			[global::ProtoBuf.ProtoMember(4, IsRequired = false, Name = @"Encoding", DataFormat = global::ProtoBuf.DataFormat.Default)]
			[global::System.ComponentModel.DefaultValue(@"utf8")]
			public string Encoding
			{
				get { return _Encoding; }
				set { _Encoding = value; }
			}

			private bool _SaveLeads = (bool)false;
			[global::ProtoBuf.ProtoMember(5, IsRequired = false, Name = @"SaveLeads", DataFormat = global::ProtoBuf.DataFormat.Default)]
			[global::System.ComponentModel.DefaultValue((bool)false)]
			public bool SaveLeads
			{
				get { return _SaveLeads; }
				set { _SaveLeads = value; }
			}

			private bool _SaveEquals = (bool)false;
			[global::ProtoBuf.ProtoMember(6, IsRequired = false, Name = @"SaveEquals", DataFormat = global::ProtoBuf.DataFormat.Default)]
			[global::System.ComponentModel.DefaultValue((bool)false)]
			public bool SaveEquals
			{
				get { return _SaveEquals; }
				set { _SaveEquals = value; }
			}
			[global::ProtoBuf.ProtoContract(Name = @"OutputMode")]
			public enum OutputMode
			{

				[global::ProtoBuf.ProtoEnum(Name = @"append", Value = 0)]
				append = 0,

				[global::ProtoBuf.ProtoEnum(Name = @"overwrite", Value = 1)]
				overwrite = 1
			}

			[global::ProtoBuf.ProtoContract(Name = @"OutputFormat")]
			public enum OutputFormat
			{

				[global::ProtoBuf.ProtoEnum(Name = @"xml", Value = 0)]
				xml = 0,

				[global::ProtoBuf.ProtoEnum(Name = @"text", Value = 1)]
				text = 1,

				[global::ProtoBuf.ProtoEnum(Name = @"proto", Value = 2)]
				proto = 2,

				[global::ProtoBuf.ProtoEnum(Name = @"mapreduce", Value = 3)]
				mapreduce = 3
			}

			private global::ProtoBuf.IExtension extensionObject;
			global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
			{ return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
		}

		[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"TArticleRef")]
		public partial class TArticleRef : global::ProtoBuf.IExtensible
		{
			public TArticleRef() { }

			private string _Name;
			[global::ProtoBuf.ProtoMember(1, IsRequired = true, Name = @"Name", DataFormat = global::ProtoBuf.DataFormat.Default)]
			public string Name
			{
				get { return _Name; }
				set { _Name = value; }
			}
			private global::ProtoBuf.IExtension extensionObject;
			global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
			{ return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
		}

		[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"TFactTypeRef")]
		public partial class TFactTypeRef : global::ProtoBuf.IExtensible
		{
			public TFactTypeRef() { }

			private string _Name;
			[global::ProtoBuf.ProtoMember(1, IsRequired = true, Name = @"Name", DataFormat = global::ProtoBuf.DataFormat.Default)]
			public string Name
			{
				get { return _Name; }
				set { _Name = value; }
			}

			private bool _NonEquality = (bool)false;
			[global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"NonEquality", DataFormat = global::ProtoBuf.DataFormat.Default)]
			[global::System.ComponentModel.DefaultValue((bool)false)]
			public bool NonEquality
			{
				get { return _NonEquality; }
				set { _NonEquality = value; }
			}
			private global::ProtoBuf.IExtension extensionObject;
			global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
			{ return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
		}

		private global::ProtoBuf.IExtension extensionObject;
		global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
		{ return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
	}

}
