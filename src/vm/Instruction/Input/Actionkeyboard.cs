using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Util;


namespace VM
{
    class Actionkeyboard : ActionBase
    {
        public override EnumActionResult Execute()
        {            
            // api.keybd_event(0x11, 0, 0, (IntPtr)0); //...CTRL key down
            // keybd_event(0x56, 0, 0, (IntPtr)0); //...V key down
            // keybd_event(0x56, 0, 0x02, (IntPtr)0); //...V key up
            // api.keybd_event(0x11, 0, 0x02, (IntPtr)0); //...CTRL key up

            string temp = m_params[0];
            string[] commands = temp.Split('.');

            switch (commands[1])
            {
                case "LeftCtrlDown":
                    NativeMethods.keybd_event(NativeMethods.VK_LCONTROL, 0x0F, 0, (UIntPtr)0);
                break;

                case "LeftCtrlUp":
                    NativeMethods.keybd_event(NativeMethods.VK_LCONTROL, 0x0F, 0 | NativeMethods.KEYEVENTF_KEYUP, (UIntPtr)0);
                break;

                default:
                    _vm.host.WriteLog("Actionkeyboard->Unrecognized command.");
                    return EnumActionResult.ERROR;
            }
            return EnumActionResult.OK;
        }

    }
}
