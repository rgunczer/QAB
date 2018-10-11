using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Util;


namespace VM
{
    class ActionTipSetup : ActionBase
    {
        public override EnumActionResult Execute()
        {
/*
            switch (m_params[1])
            {
                case "BorderWidth":
                    FrmTip._BorderWidht = Convert.ToInt32(m_params[3]);
                break;

                case "FontStyle":
                {
                    switch (m_params[3])
                    {
                        case "Bold":
                            FrmTip.m_FontStyle = FontStyle.Bold;
                        break;

                        case "Regular":
                            FrmTip.m_FontStyle = FontStyle.Regular;
                        break;

                        case "Italic":
                            FrmTip.m_FontStyle = FontStyle.Italic;
                        break;

                        case "Strikeout":
                            FrmTip.m_FontStyle = FontStyle.Strikeout;
                        break;

                        case "Underline":
                            FrmTip.m_FontStyle = FontStyle.Underline;
                        break;
                    }
                }
                break;

                case "FontFamily":
                    FrmTip.m_FontFamily = m_params[3];
                break;

                case "FontSize":
                    FrmTip.m_FontSize = Convert.ToInt32(m_params[3]);
                break;

                case "FontColor":
                    FrmTip.m_ColorFont = Color.FromArgb(Convert.ToInt32(m_params[3]), Convert.ToInt32(m_params[5]), Convert.ToInt32(m_params[7]));
                break;

                case "LeftColor":
                    FrmTip.m_ColorLeft = Color.FromArgb(Convert.ToInt32(m_params[3]), Convert.ToInt32(m_params[5]), Convert.ToInt32(m_params[7]));
                break;

                case "RightColor":
                    FrmTip.m_ColorRight = Color.FromArgb(Convert.ToInt32(m_params[3]), Convert.ToInt32(m_params[5]), Convert.ToInt32(m_params[7]));
                break;

                case "BorderColor":
                    FrmTip.m_ColorBorder = Color.FromArgb(Convert.ToInt32(m_params[3]), Convert.ToInt32(m_params[5]), Convert.ToInt32(m_params[7]));
                break;

                default:
                    UtilSys.MessageBoxError("Unknown command [" + m_params[1] + "]");
                return EnumActionResult.ERROR;                
            }
 */ 
            return EnumActionResult.OK;
        }
    }
}
