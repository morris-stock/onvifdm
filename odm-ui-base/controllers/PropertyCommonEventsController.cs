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
using odm.onvif;
using odm.models;
using odm.utils.controlsUIProvider;
using onvif;

namespace odm.controllers {
	public class PropertyCommonEventsController : BasePropertyController {
		Session CurrentSession { get; set; }
		IDisposable _subscription;

		public PropertyCommonEventsController() { }

		public override void CreateController(Session session, ChannelModel chan) {
			var eventList = WorkflowController.Instance.GetMainFrameController().GetEventList(new VideoSourceToken("device"));
			UIProvider.Instance.GetCommonEventsProvider().InitView(eventList);

			WorkflowController.Instance.GetMainFrameController().EventAction = EventHandler;
			WorkflowController.Instance.GetMainFrameController().RemoveEventAction = RemoveEvent;
		}

		public void EventHandler(EventDescriptor evDescr) {
			if (evDescr.ChannelID.value == "device")
				UIProvider.Instance.GetCommonEventsProvider().AddEvent(evDescr);
		}
		public void RemoveEvent(EventDescriptor evDescr) {
			if (evDescr.ChannelID.value == "device")
				UIProvider.Instance.GetCommonEventsProvider().RemoveEvent(evDescr);
		}
		
		protected override void ApplyChanges() {}
		protected override void CancelChanges() { }
		public override void ReleaseAll() {
			UIProvider.Instance.ReleaseCommonEventsProvider();
			if (_subscription != null) _subscription.Dispose();
		}
		protected override void LoadControl() { }
	}
}
