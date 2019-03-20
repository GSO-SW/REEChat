﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REEChatDLL
{
	public partial class WatermarkTextBox : Component
	{
		public WatermarkTextBox()
		{
			InitializeComponent();
		}

		public WatermarkTextBox(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}
	}
}