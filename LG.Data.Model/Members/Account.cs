using LG.Data.Models;
using LG.Services.ACS;
using System;
using System.Runtime.CompilerServices;

namespace LG.Data.Models.Members
{
	public class Account : BaseModel
	{
		public int AccountID
		{
			get;
			set;
		}

		public LG.Services.ACS.AccountInfo AccountInfo
		{
			get;
			set;
		}

		public long ClientRID
		{
			get;
			set;
		}

		public string CoverageCode
		{
			get;
			set;
		}

		public DateTime EffectiveDate
		{
			get;
			set;
		}

		public string GroupNumber
		{
			get;
			set;
		}

		public long GroupRID
		{
			get;
			set;
		}

		public string MemberNumber
		{
			get;
			set;
		}

		public int MembershipPlanID
		{
			get;
			set;
		}

		public long RID
		{
			get;
			set;
		}

		public Account()
		{
		}
	}
}