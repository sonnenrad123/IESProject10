﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
	public class Equipment : PowerSystemResource
	{		
		private bool aggregate;
		private bool normallyInService;
						
		public Equipment(long globalId) : base(globalId) 
		{
		}
	
		public bool Aggregate
		{
			get
			{
				return aggregate;
			}

			set
			{
				aggregate = value;
			}
		}

		public bool NormallyInService
		{
			get 
			{
				return normallyInService; 
			}
			
			set
			{
				normallyInService = value; 
			}
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Equipment x = (Equipment)obj;
				return ((x.aggregate == this.aggregate) &&
						(x.normallyInService == this.normallyInService));
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#region IAccess implementation

		public override bool HasProperty(ModelCode property)
		{
			switch (property)
			{
				case ModelCode.EQUIP_AGGREGATE:
				case ModelCode.EQUIP_NORMINSERVICE:
		
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.EQUIP_AGGREGATE:
					property.SetValue(aggregate);
					break;

				case ModelCode.EQUIP_NORMINSERVICE:
					property.SetValue(normallyInService);
					break;			

				default:
					base.GetProperty(property);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.EQUIP_AGGREGATE:					
					aggregate = property.AsBool();
					break;

				case ModelCode.EQUIP_NORMINSERVICE:
					normallyInService = property.AsBool();
					break;
			
				default:
					base.SetProperty(property);
					break;
			}
		}		

		#endregion IAccess implementation
	}
}
