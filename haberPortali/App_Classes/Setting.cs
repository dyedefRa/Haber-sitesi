using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;


namespace haberPortali.App_Classes
{
    public static class Setting
    {
      
        public static Size KucukResimWH()
        {
            Size temp = new Size();
            temp.Width = Convert.ToInt32(ConfigurationManager.AppSettings["KucukResimWidth"]);
            temp.Height = Convert.ToInt32(ConfigurationManager.AppSettings["KucukResimHeight"]);
            return temp;
        }
        public static Size BuyukResimWH()
        {
         
           return new Size(Convert.ToInt32(ConfigurationManager.AppSettings["BuyukResimWidth"]), Convert.ToInt32(ConfigurationManager.AppSettings["BuyukResimHeight"]));
         
            
        }

       
    }
}