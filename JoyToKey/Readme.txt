======================================================================

                          JoyToKey Ver3.7.4

                  Copyright(C) 1999-2002, Ryo Ohkubo

======================================================================


1. Purposes
-----------
This software is a keyboard emulator for joysticks.
It converts joystick input into keyboard input (and mouse input).

Use it when you want to control an application with joysticks that
doesn't support joystick input. If you wish, you can control even
Word, Excel, etc. with joysticks!


2. Features
-----------
* Configuration for maximum 16 joysticks.
  (6 axes, 2 POV switches and 32 buttons for each joystick)

* Multiple configuration files.
  You can make lots of configuration files and choose it at any time.

* Support for many useful features...
  - Automatic shooting of buttons.
  - Mouse emulation (including wheel rotation).
  - "Adjust mouse movements" function.
    When it's pressed, mouse movement(or wheel rotation) becomes
    temporarily faster (or slower).

* "Switch to the other configuration file" function.
  You can switch to and activate the other configuration file
  with the button which is assigned to this function.

* "Use the setting of other joystick number temporarily" function.
  (Something like "shift" command of SNESKey.
   For example, you may usually use joystick1 for keyboard emulation,
   but during this button being pressed, joystick3's configuration
   (that emulates mouse or something) will temporarily be used.
   Note that joystick3 is not a real joystick, it's a virtual device
   to .)


3. Installation
---------------
Simply unzip the archive file into some folder.
To uninstall this software, you only need to remove that folder.
(DirectX 6.0 or above is necessary for the use of JoyToKey)


4. Usage
--------
* Push "Create" button and make a new configuration

* Choose the button of the joystick from the list
  and double click or press enter key.

* "Use the setting of other joystick number temporarily" feature may
  be hard to understand at first, but it is very useful for the joystick
  which has few buttons.

  During the button be pressed, the rest of button's assignment are
  temporarily changed to the other assignment. So you can virtually use 
  twice or more number of buttons.

* Configuration file (*.cfg) is simply a text file. So, If you want to
  rename the configuration, copy the configuration, ..., etc.
  terminate the "JoyToKey" and simply rename or copy the file (*.cfg).


5. Misc
-------
* If you are a new user and you have some trouble configuring JoyToKey,
  please follow the instructions below.

  1. I recommend you to try JoyToKey with "Notepad.exe".

  2. Please configure your joypad with cursor key and "a", "b" keys.
     And then try the joypad on a Notepad window.  Characters "a", "b" 
     and the movement of a cursor should be observed.
     (Be sure to keep the JoyToKey program runnnig,
      iconized in the task tray at the bottom of the desktop.)

  3. If it doesn't work, consult the "control panel" of Windows.
     At the "Gamepad" item, please check your joypad to be calibrated 
     correctly.


* If you want to control Internet Explorer 4.0 with joysticks,
  configure as follows...

  "Back" : Alt + Left
Å@"Close Window" : Alt + F + C
Å@"Move To Menu" : Alt + F

  And you'll want to add Wheel rotation, Mouse movement etc., too.

  Other Windows applications will also be configured like this.


* Known limitations
  These won't work.
    1. All key emulation for Windows application using DirectInput
    2. Alt or Ctrl key emulation for MS-DOS application
    3. The difference between left and right Alt/Ctrl/Win keys


6. Changes
----------
Ver3.7.4
* Under Windows 2000 or later (including XP), JoyToKey now supports
  some applications using DirectInput.

* Choosing L-Shift, R-Shift, L-Ctrl, ... from the list, you can make
  a distinction between left and right keys.
  (Be careful to select L-Shift or R-Shift or (normal) Shift!
   You have to choose the correct one for the application!)


Ver3.7.3
* Support for POV switches.

* Support for "Switch to the other configuration file" function.

* The configuration file can be easily selected and switched to
  at task tray menu, without opening and activating JoyToKey window.

* The firstly activated configuration file can now be specified
  by command line option at start up time.

* Analog stick can be configured in detail by modifying JoyToKey.ini
  file directly:

  "AnalogDeadZone"  (ranges 0 to 10000: default 1000)
    The center area where subtle input from analog stick is ignored.

  "AnalogSaturation"  (ranges 0 to 10000: default 10000)
    The surrounding area from which additional inputs are saturated.


Ver3.6.1
* bug fix for starting iconified setting.

Ver3.6
* Automatic shooting support for mouse's button emulation.

Ver3.0-3.5
* Old version


7. Others
---------
This product comes with no warranty. It is freeware, but use it at your
own risk. The author takes no responsibilities of any sort related to
the use of this product.

Please note that this software doesn't support some applications, 
especially games, which use DirectInput(DirectX) APIs.

If you have some comments, suggestions or bug-report, feel free to email
me. (I can't speak English well, though...)

Thank you for using this software.

URL: http://www.vector.co.jp/authors/VA016823/
E-Mail: r-ohkubo@ijk.com
