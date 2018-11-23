namespace CmdOsu.Assets
{
	static class DifficultyCalc
	{
		public static float GetCircleArea(float cs)
		{
			// Not the actual exact formula but works just as good
			return (float)( 23.05 - (cs - 7) * 4.4825);
		}

		public static float GetHitWindow300(float od)
		{
			if (od > 10 | od < 0)
			{
				throw new System.ArgumentOutOfRangeException($"Od must be between 0 and 10. Ar was {od}");
			}

			return (79 - (od * 6) + 0.5f) / 1000f;
		}

		public static float GetHitWindow100(float od)
		{
			if (od > 10 | od < 0)
			{
				throw new System.ArgumentOutOfRangeException($"Od must be between 0 and 10. Ar was {od}");
			}

			return (139 - (od * 8) + 0.5f) / 1000f;
		}

		public static float GetHitWindow50(float od)
		{
			if (od > 10 | od < 0)
			{
				throw new System.ArgumentOutOfRangeException($"Od must be between 0 and 10. Ar was {od}");
			}

			return (199 - (od * 10) + 0.5f) / 1000f;
		}

		public static float GetObjectLifeTime(float ar)
		{
			if (ar > 10 | ar < 0)
			{
				throw new System.ArgumentOutOfRangeException($"Ar must be between 0 and 10. Ar was {ar}");
			}


			if (ar > 5)
			{
				return (1200 - (ar - 5) * 150) / 1000f;
			}
			else
			{
				return (1800 - ar * 120) / 1000f;
			}

			// Mod calculations
			// I don't want to dig through this nightmare if not necessary

			//if (this.bolEZ)
			//{
			//	num1 = this.EZ(num3, num2, num);
			//	num = num4 / 2;
			//	this.lblMMS.Text = Convert.ToString(num1);
			//	this.lblModAR.Text = Convert.ToString(num);
			//}
			//if (this.bolHT)
			//{
			//	num1 = this.HT(num3, num2);
			//	if (num1 < 1200)
			//	{
			//		num = (1200 - num1) / 150 + 5;
			//		this.lblMMS.Text = Convert.ToString(num1);
			//		this.lblModAR.Text = Convert.ToString(num);
			//	}
			//	else
			//	{
			//		num = (1800 - num1) / 120;
			//		this.lblMMS.Text = Convert.ToString(num1);
			//		this.lblModAR.Text = Convert.ToString(num);
			//	}
			//}
			//if (this.bolHR)
			//{
			//	num1 = this.HR(num3, num2, num);
			//	if (num3 > 7.1)
			//	{
			//		num = 10;
			//		this.lblMMS.Text = Convert.ToString(num1);
			//		this.lblModAR.Text = Convert.ToString(num);
			//	}
			//	else
			//	{
			//		num = num3 * 1.4;
			//		this.lblMMS.Text = Convert.ToString(num1);
			//		this.lblModAR.Text = Convert.ToString(num);
			//	}
			//}
			//if (this.bolDT)
			//{
			//	num1 = this.DT(num3, num2);
			//	if (num1 < 1200)
			//	{
			//		num = (1200 - num1) / 150 + 5;
			//		this.lblMMS.Text = Convert.ToString(num1);
			//		this.lblModAR.Text = Convert.ToString(num);
			//	}
			//	else
			//	{
			//		num = (1800 - num1) / 120;
			//		this.lblMMS.Text = Convert.ToString(num1);
			//		this.lblModAR.Text = Convert.ToString(num);
			//	}
			//}
			//if (this.bolEZ & this.bolHT)
			//{
			//	num = num4 / 2;
			//	if (num > 5)
			//	{
			//		num1 = (1200 - (num - 5) * 150) * 1.5;
			//		if (num1 < 1200)
			//		{
			//			num = (1200 - num1) / 150 + 5;
			//			this.lblMMS.Text = Convert.ToString(num1);
			//			this.lblModAR.Text = Convert.ToString(num);
			//		}
			//		else
			//		{
			//			num = (1800 - num1) / 120;
			//			this.lblMMS.Text = Convert.ToString(num1);
			//			this.lblModAR.Text = Convert.ToString(num);
			//		}
			//	}
			//	else
			//	{
			//		num1 = (1800 - num * 120) * 1.5;
			//		if (num1 < 1200)
			//		{
			//			num = (1200 - num1) / 150 + 5;
			//			this.lblMMS.Text = Convert.ToString(num1);
			//			this.lblModAR.Text = Convert.ToString(num);
			//		}
			//		else
			//		{
			//			num = (1800 - num1) / 120;
			//			this.lblMMS.Text = Convert.ToString(num1);
			//			this.lblModAR.Text = Convert.ToString(num);
			//		}
			//	}
			//}
			//if (this.bolEZ & this.bolDT)
			//{
			//	num = num4 / 2;
			//	if (num > 5)
			//	{
			//		num1 = (1200 - (num - 5) * 150) / 1.5;
			//		if (num1 < 1200)
			//		{
			//			num = (1200 - num1) / 150 + 5;
			//			this.lblMMS.Text = Convert.ToString(num1);
			//			this.lblModAR.Text = Convert.ToString(num);
			//		}
			//		else
			//		{
			//			num = (1800 - num1) / 120;
			//			this.lblMMS.Text = Convert.ToString(num1);
			//			this.lblModAR.Text = Convert.ToString(num);
			//		}
			//	}
			//	else
			//	{
			//		num1 = (1800 - num * 120) / 1.5;
			//		if (num1 < 1200)
			//		{
			//			num = (1200 - num1) / 150 + 5;
			//			this.lblMMS.Text = Convert.ToString(num1);
			//			this.lblModAR.Text = Convert.ToString(num);
			//		}
			//		else
			//		{
			//			num = (1800 - num1) / 120;
			//			this.lblMMS.Text = Convert.ToString(num1);
			//			this.lblModAR.Text = Convert.ToString(num);
			//		}
			//	}
			//}
			//if (this.bolHR & this.bolHT)
			//{
			//	if (num3 > 7.1)
			//	{
			//		num = 10;
			//		num1 = (1200 - (num - 5) * 150) * 1.5;
			//		if (num1 < 1200)
			//		{
			//			num = (1200 - num1) / 150 + 5;
			//			this.lblMMS.Text = Convert.ToString(num1);
			//			this.lblModAR.Text = Convert.ToString(num);
			//		}
			//		else
			//		{
			//			num = (1800 - num1) / 120;
			//			this.lblMMS.Text = Convert.ToString(num1);
			//			this.lblModAR.Text = Convert.ToString(num);
			//		}
			//	}
			//	else
			//	{
			//		num = num3 * 1.4;
			//		if (num > 5)
			//		{
			//			num1 = (1200 - (num - 5) * 150) * 1.5;
			//			if (num1 < 1200)
			//			{
			//				num = (1200 - num1) / 150 + 5;
			//				this.lblMMS.Text = Convert.ToString(num1);
			//				this.lblModAR.Text = Convert.ToString(num);
			//			}
			//			else
			//			{
			//				num = (1800 - num1) / 120;
			//				this.lblMMS.Text = Convert.ToString(num1);
			//				this.lblModAR.Text = Convert.ToString(num);
			//			}
			//		}
			//		else
			//		{
			//			num1 = (1800 - num * 120) * 1.5;
			//			num = (1800 - num1) / 120;
			//			this.lblMMS.Text = Convert.ToString(num1);
			//			this.lblModAR.Text = Convert.ToString(num);
			//		}
			//	}
			//}
			//if (this.bolHR & this.bolDT)
			//{
			//	if (num3 > 7.1)
			//	{
			//		num = 11;
			//		num1 = 1200 - (num - 5) * 150;
			//		this.lblMMS.Text = Convert.ToString(num1);
			//		this.lblModAR.Text = Convert.ToString(num);
			//	}
			//	else
			//	{
			//		num = num3 * 1.4;
			//		if (num > 5)
			//		{
			//			num1 = (1200 - (num - 5) * 150) / 1.5;
			//			num = (1200 - num1) / 150 + 5;
			//			this.lblMMS.Text = Convert.ToString(num1);
			//			this.lblModAR.Text = Convert.ToString(num);
			//		}
			//		else
			//		{
			//			num1 = (1800 - num * 120) / 1.5;
			//			if (num1 < 1200)
			//			{
			//				num1 = (1200 - (num - 5) * 150) / 1.5;
			//				if (num1 < 1200)
			//				{
			//					num = (1200 - num1) / 150 + 5;
			//					this.lblMMS.Text = Convert.ToString(num1);
			//					this.lblModAR.Text = Convert.ToString(num);
			//				}
			//				else
			//				{
			//					num = (1800 - num1) / 120;
			//					this.lblMMS.Text = Convert.ToString(num1);
			//					this.lblModAR.Text = Convert.ToString(num);
			//				}
			//			}
			//			else
			//			{
			//				num = (1800 - num1) / 120;
			//				this.lblMMS.Text = Convert.ToString(num1);
			//				this.lblModAR.Text = Convert.ToString(num);
			//			}
			//		}
			//	}
			//}
			//if (!this.bolEZ & !this.bolHR & !this.bolDT & !this.bolHT)
			//{
			//	if (num3 > 5)
			//	{
			//		this.lblMMS.Text = "";
			//		this.lblModAR.Text = "";
			//		this.lblBMS.Text = Convert.ToString(1200 - (num3 - 5) * 150);
			//	}
			//	else
			//	{
			//		this.lblMMS.Text = "";
			//		this.lblModAR.Text = "";
			//		this.lblBMS.Text = Convert.ToString(1800 - num3 * 120);
			//	}
			//}
		}
	}
}

