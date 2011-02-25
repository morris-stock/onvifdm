﻿#region License and Terms
//----------------------------------------------------------------------------------------------------------------
// Copyright (C) 2010 Synesis LLC and/or its subsidiaries. All rights reserved.
//
// Commercial Usage
// Licensees  holding  valid ONVIF  Device  Manager  Commercial  licenses may use this file in accordance with the
// ONVIF  Device  Manager Commercial License Agreement provided with the Software or, alternatively, in accordance
// with the terms contained in a written agreement between you and Synesis LLC.
//
// GNU General Public License Usage
// Alternatively, this file may be used under the terms of the GNU General Public License version 3.0 as published
// by  the Free Software Foundation and appearing in the file LICENSE.GPL included in the  packaging of this file.
// Please review the following information to ensure the GNU General Public License version 3.0 
// requirements will be met: http://www.gnu.org/copyleft/gpl.html.
// 
// If you have questions regarding the use of this file, please contact Synesis LLC at onvifdm@synesis.ru.
//----------------------------------------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using odm.ui.controls;
using odm.controllers;
using odm.models;
using odm.utils.controlsUIProvider;

namespace odm.controlsUIProvider {
	public class AntishakerProvider : BaseUIProvider, IAntishakerProvider {
		PropertyAntishaker _antishaker;

		public void InitView(AntishakerModel devModel, DataProcessInfo datProcInfo, Action ApplyChanges, Action CancelChanges) {
			_antishaker = new PropertyAntishaker(devModel) { 
				Save = ApplyChanges, 
				Cancel = CancelChanges, 
				onBindingError = BindingError 
			};
			if (datProcInfo != null) {
				_antishaker.memFile = datProcInfo.VideoProcessFile;
				_antishaker.InitPlayback();
			}

			WPFUIProvider.Instance.MainFrameProvider.AddPropertyControl(_antishaker);
		}
		public override void ReleaseUI() {
			if (_antishaker != null) {
				_antishaker.ReleaseAll();
				_antishaker = null;
			}
		}
	}
}