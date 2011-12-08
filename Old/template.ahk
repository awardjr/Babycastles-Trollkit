; Solace

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; SETUP
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

; Game Variables
Game_Window_Title = VVVVVV
Game_Path = "C:\Arcade\Games\VVVVVV\VVVVVV.exe"
Game_JoyToKey = "C:\Arcade\Games\VVVVVV\JoyToKey.exe"

; General Interface Settings
Start_With_Hidden_Cursor = 0
Reposition_Mouse = 0
;Game_Send_Full_Screen = 1
Focus_Click = 0
Mouse_Position_X = 250
Mouse_Position_Y = 250

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; INITIALIZATION
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

; Windows sometimes runs startup without being ready for it.
Sleep, 10000
if (Start_With_Hidden_Cursor = 1)
{
	SystemCursor("Toggle")
}
if (Reposition_Mouse = 1)
{
	MouseMove, %Mouse_Position_X%, %Mouse_Position_Y%
}

SetTitleMatchMode, 2
SendMode Input

;Run %Game_JoyToKey%,,Min
;WinHide ahk_class Shell_TrayWnd
Sleep 100
Restart_Game()

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; RESTART BUTTON
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

Restart_Key_Delay = 200

; Start clicks also restarts the game when held down.
Joy9::
Get_Hold_State("Joy9")
Return

Joy10::
Get_Hold_State("Joy10")
Return

Joy11::
Get_Hold_State("Joy11")
Return

Get_Hold_State(Hold_Key)
{
	global
	Restart_Key_Counter = 0
	Loop
	{
		Restart_Key_Counter++
		Sleep, 10
		Restart_Key_State:= GetKeyState(Hold_Key, "P")	
		if (Restart_Key_State = 0)
		{
	;		//MsgBox, "BYE" + %Restart_Key_Counter%
			Restart_Key_Counter = 0

			Return
		}
		else if (Restart_Key_Counter > Restart_Key_Delay)
		{
	;		//MsgBox, HI + %Restart_Key_Counter%
			Restart_Key_Counter = 0
			Restart_Game()
			Return
		}
	}
	Return
}

Restart_Game()
{
	global Game_Window_Title
	global Game_Path
	global Game_Send_Full_Screen
	global Focus_Click
	
;	//Kill All Programs with this name
	Loop
	{
		IfWinNotExist, %Game_Window_Title%
			break				
		WinKill, %Game_Window_Title%
	}
	
;	//Restart Program
	Run %Game_Path%,,Max
	if (Game_Send_Full_Screen = 1)
	{
		WinWait, %Game_Window_Title%
		Sleep, 50
		Send ^{f}
	}

	if (Focus_Click = 1)
	{
		Sleep, 200
		Click
	}
}


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; SYSTEM KEY MAPPINGS
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

; Press Win-A to Reload Autohotkey
#a::Reload

; Press Win-C to Toggle Cursor
#c::SystemCursor("Toggle")

#h::WinShow ahk_class Shell_TrayWnd

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; BABYCASTLES LIBRARY
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

SystemCursor(OnOff=1)   ; INIT = "I","Init"; OFF = 0,"Off"; TOGGLE = -1,"T","Toggle"; ON = others
{
    static AndMask, XorMask, $, h_cursor
        ,c0,c1,c2,c3,c4,c5,c6,c7,c8,c9,c10,c11,c12,c13 ; system cursors
        , b1,b2,b3,b4,b5,b6,b7,b8,b9,b10,b11,b12,b13   ; blank cursors
        , h1,h2,h3,h4,h5,h6,h7,h8,h9,h10,h11,h12,h13   ; handles of default cursors
    if (OnOff = "Init" or OnOff = "I" or $ = "")       ; init when requested or at first call
    {
        $ = h                                          ; active default cursors
        VarSetCapacity( h_cursor,4444, 1 )
        VarSetCapacity( AndMask, 32*4, 0xFF )
        VarSetCapacity( XorMask, 32*4, 0 )
        system_cursors = 32512,32513,32514,32515,32516,32642,32643,32644,32645,32646,32648,32649,32650
        StringSplit c, system_cursors, `,
        Loop %c0%
        {
            h_cursor   := DllCall( "LoadCursor", "uint",0, "uint",c%A_Index% )
            h%A_Index% := DllCall( "CopyImage",  "uint",h_cursor, "uint",2, "int",0, "int",0, "uint",0 )
            b%A_Index% := DllCall("CreateCursor","uint",0, "int",0, "int",0
                , "int",32, "int",32, "uint",&AndMask, "uint",&XorMask )
        }
    }
    if (OnOff = 0 or OnOff = "Init" or OnOff = "I" or $ = "" OnOff = "Off" or $ = "h" and (OnOff < 0 or OnOff = "Toggle" or OnOff = "T"))
        $ = b  ; use blank cursors
    else
        $ = h  ; use the saved cursors

    Loop %c0%
    {
        h_cursor := DllCall( "CopyImage", "uint",%$%%A_Index%, "uint",2, "int",0, "int",0, "uint",0 )
        DllCall( "SetSystemCursor", "uint",h_cursor, "uint",c%A_Index% )
    }
}



;;;;;;;;;;;;;;;;;;;;;;
; CLEANUP
;;;;;;;;;;;;;;;;;;;;;;

OnExit, Cleanup
return

Cleanup:
SystemCursor("On")
ExitApp