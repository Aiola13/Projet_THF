using System;
using UnityEngine;

/// <summary>
/// Before
/// </summary>
namespace VolumicVR
{
	public interface IPrinterStand
	{
		event EventHandler	PrintEndedEvent;

		PrintedObject		ObjectToPrint			{ get; set; }
		GameObject			ActualPrintedSTL		{ get; }

		float				ActualSTLPrintTime		{ get; }
		int					ActualSTLTotalSteps		{ get; }
		int					ActualSTLCurrentStep	{ get; }

		float				TimeMultiplier			{ get; set; }


		/// <summary>
		/// Sets the object to print
		/// </summary>
		/// <param name="printedObjectNum">PrintedObject enum index</param>
		void				SetSTLToPrint ( int printedObject );
		/// <summary>
		/// Sets the object to print
		/// </summary>
		/// <param name="printedObjectNum">Object to print</param>
		void SetSTLToPrint ( PrintedObject printedObject );
		void				StartPrinting ();
		void				PausePrint ();
		void				UnpausePrint ();		
	}
}
/*
		   ;               ,           
         ,;                 '.         
        ;:                   :;        
       ::                     ::       
       ::                     ::       
       ':                     :        
        :.                    :        
     ;' ::                   ::  '     
    .'  ';                   ;'  '.    
   ::    :;                 ;:    ::   
   ;      :;.             ,;:     ::   
   :;      :;:           ,;"      ::   
   ::.      ':;  ..,.;  ;:'     ,.;:   
    "'"...   '::,::::: ;:   .;.;""'    
        '"""....;:::::;,;.;"""         
    .:::.....'"':::::::'",...;::::;.   
   ;:' '""'"";.,;:::::;.'""""""  ':;   
  ::'         ;::;:::;::..         :;  
 ::         ,;:::::::::::;:..       :: 
 ;'     ,;;:;::::::::::::::;";..    ':.
::     ;:"  ::::::"""'::::::  ":     ::
 :.    ::   ::::::;  :::::::   :     ; 
  ;    ::   :::::::  :::::::   :    ;  
   '   ::   ::::::....:::::'  ,:   '   
    '  ::    :::::::::::::"   ::       
       ::     ':::::::::"'    ::       
       ':       """""""'      ::       
        ::                   ;:        
        ':;                 ;:"        
          ';              ,;'          
            "'           '"            
              '
*/
