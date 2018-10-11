using System;
using System.Collections.Generic;
using System.Text;


namespace VM
{
    class Actiondatetime : ActionBase
    {
        public override EnumActionResult Execute()
        {
            string type = _vm.variables.Get(m_params[0]);
            string amount = _vm.variables.Get(m_params[1]);
            string frmt = _vm.variables.Get(m_params[3]);
            string varName = m_params[5];
                        
            string datum = _vm.variables.Get(varName);

            DateTime dt = DateTime.Parse(datum);
            DateTime dtNew = dt;
           
            switch (type)
            {
                case "datetime.AddDays":
                {
                    double days = Convert.ToDouble(amount);
                    dtNew = dt.AddDays(days);                    
                }
                break;

                case "datetime.AddMonths":
                {
                    int months = Convert.ToInt32(amount);
                    dtNew = dt.AddMonths(months);
                }
                break;

                case "datetime.AddYears":
                {
                    int years = Convert.ToInt32(amount);
                    dtNew = dt.AddYears(years);
                }
                break;

                case "datetime.AddHours":
                {
                    double hours = Convert.ToDouble(amount);
                    dtNew = dt.AddHours(hours);
                }
                break;

                case "datetime.AddMinutes":
                {
                    double minutes = Convert.ToDouble(amount);
                    dtNew = dt.AddMinutes(minutes);
                }
                break;

                case "datetime.AddSeconds":
                {
                    double seconds = Convert.ToDouble(amount);
                    dtNew = dt.AddSeconds(seconds);
                }
                break;

                default:
                    throw new Exception("Unrecognized type [" + type + "]");
            }

            string newDatum = String.Format("{0:" + frmt + "}", dtNew);
            _vm.variables.Update(varName, newDatum);

            return EnumActionResult.OK;
        }

        public override string ToString()
        {
            string tmp = string.Empty;

            for (int i = 0; i < m_params.Count; ++i)
            {
                tmp += " " + m_params[i];
            }
            return (tmp.Trim());
        }

    }
}
