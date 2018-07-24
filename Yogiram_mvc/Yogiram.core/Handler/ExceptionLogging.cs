﻿using System;
using System.IO;
using context = System.Web.HttpContext;

/// <summary>
/// Summary description for ExceptionLogging
/// </summary>
public static class ExceptionLogging
{
 private  static String ErrorlineNo, Errormsg, extype, exurl, hostIp,ErrorLocation, HostAdd;      
    public static void SendErrorToText(Exception ex)
    {
        var line = Environment.NewLine;

        ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
        Errormsg = ex.GetType().Name.ToString();
        extype = ex.GetType().ToString();
        exurl = context.Current.Request.Url.ToString();
        ErrorLocation = ex.Message.ToString();

        try
        {
            string filepath = context.Current.Server.MapPath("~/Exception/");

            if (!Directory.Exists(filepath))            
                Directory.CreateDirectory(filepath);
            
            filepath = filepath + DateTime.Today.ToString("dd-MMM-yy") + ".txt";

            if (!File.Exists(filepath))    
                File.Create(filepath).Dispose();

            using (StreamWriter sw = File.AppendText(filepath))
            {
                string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:"  + " " + Errormsg + line 
                             + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line 
                             + "User Host IP:" + " " + hostIp;

                sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                sw.WriteLine("-------------------------------------------------------------------------------------");
                sw.WriteLine(error);
                sw.WriteLine("--------------------------------*End*------------------------------------------");
                sw.WriteLine(line);
                sw.Flush();
                sw.Close();

            }

        }
        catch (Exception e)
        {
            e.ToString();

        }
    }
}