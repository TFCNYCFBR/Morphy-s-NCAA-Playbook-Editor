Morphy-s-NCAA-Playbook-Editor

Basic Video for Custom Playbooks: https://www.youtube.com/watch?v=JuDwD43noc4

Instructional Google Doc for Playbook Editor: https://docs.google.com/document/d/1HkSXewOHgYPUl_6kk-8bEBma3RYwkzGtqDd5NIFmXy4/edit?usp=sharing

The content below is directly pasted from the instructional Google Doc

How to Use New Plays in Your NCAA 14 Game
This is really simple. Go on over to the custom playbooks option in the game menu. If you haven’t created one before, create one and pick any starter playbook from any team (it doesn’t really matter). Then go in, delete all the plays within that playbook and choose which plays and formations you want. You can even order which plays appear first in each formation and change your audibles. Then save it and you’ll then be able to use it in any game. Keep in mind there’s a glitch for some people that has been in the game since NCAA 11. It causes certain formations to disappear when you’re using no-huddle, or to be replaced by another formation (e.g. it will say SHOTGUN Y TRIPS but it will actually show plays from Shotgun Spread). The glitch only applies to the last four or five shotgun formations that you added within your playbook. To work around this, put in the shotgun formations you will use the least in no-huddle last. Again, this is an issue with the game itself, not the mod. In 95% of the issues you may encounter, it will be with the game itself. I will be happy to elaborate more in the future.
How to Edit Your Own Plays
NOTE: Currently, this is only for PS3 and PC users. For XBOX users, if you can figure out a way to play off of a USB drive, or replace game files with a USB drive, then it should work. Frankly, I don’t have much experience with jailbroken XBOX 360s so it might be possible.

For PS3 USERS -- OFF HDD (Playing off of USB is an easier process with less steps -- see that process below)
Go on the Discord and download the AST editor in #tools
Plug a USB into your PS3. Boot up the PS3 and get into Multiman. In the menu, go all the way to the left and click on the desktop manager (the icon looks like a desktop). 
Once you’re in the desktop-looking app, create your own file and name it whatever you want. Open the file and press the “..” file until you reach /dev_hdd0. Then click the files in this order: GAMES/BLUS31159-(NCAA Football 14)/PS3_GAME/USRDIR
You should now see a “..” file, a “data” file, an EBOOT.BIN file, and five .ast files. Copy the qkl_misc.ast file 
Once that’s copied, press the “..” icon until you reach the very beginning. You should see a dev_usb000 option. Click it and paste the file this folder
Shut off your PS3 and pull out your USB. Insert it into your desktop/laptop. Copy and paste the qkl_misc.ast file onto your desktop. 
Open up the AST editor and click that qkl_misc.ast file -- go to the file 7, a db file, right click, press extract selected and save the file anywhere you feel comfortable (on your desktop is fine, or make your own folder)
Open up the playbook editor exe, choose that db file, edit away and save
Open back up the qkl_misc.ast file using the AST editor, right click on the 7th file, press replace selected, and choose your edited db file; you have now replaced the default file with your custom one
Go to the editor’s top left tab and press file, save as, and replace save the qkl_misc.ast file on your USB
Eject USB, enter it into the game and pull up Multiman and then desktop manager
Open up your empty created folder, go to the exact same place that you were before (GAMES/BLUS31159-[NCAA Football 14]/PS3_GAME/USRDIR) and delete the current qkl_misc.ast file that’s sitting with the other .ast files
Copy your qkl_misc.file from your usb and paste it into the other folder where you just deleted the file
Press the GAMES icon on the desktop, run the game off of HDD and make sure your changes copied over

You can *also* make this process a lot simpler by copying the entire “GAMES” folder onto your USB and playing off of the USB. That way when you make a change to the misc file, just save and replace it with the original misc file in GAMES/BLUS31159-(NCAA Football 14)/PS3_GAME/USRDIR/qkl_misc.ast. Then plug in your USB into the console and play right off the USB.
For PS3 USERS -- OFF USB (Only if you still have the original GAMES/BLUS31159-[NCAA Football 14] file on your USB from when you first dumped the game)
Go on the Discord and download the AST editor in #tools
Plug a USB into your PS3. Boot up the PS3 and get into Multiman. In the menu, go all the way to the left and click on the desktop manager (the icon looks like a desktop). 
Once you’re in the desktop-looking app, create your own file and name it whatever you want. Open the file and press the “..” file until you reach /dev_hdd0. Then click the files in this order: GAMES/BLUS31159-(NCAA Football 14)/PS3_GAME/USRDIR
You should now see a “..” file, a “data” file, an EBOOT.BIN file, and five .ast files. Copy all five ast files (boot, fe2ig, interface, misc, stream) 
Once they’re copied, press the “..” icon until you reach the very beginning. You should see a dev_usb000 option. Click it and paste the files this folder
Shut off your PS3 and pull out your USB. Insert it into your desktop/laptop. Copy all five ast files and go to this folder:USB Drive > GAMES>BLUS31159-[NCAA Football 14] > PS3_GAME > USRDIR
Paste all five files in this folder, replacing the five current ones
You will only have to do steps 4-7 once until a new version of the mod comes out since you will be playing off of your USB now. In all subsequent times until you have to install the new version of the mod, you will just have to replace the misc file on your USB, plug it in and play; I suggest keeping the latest version of qkl_misc.ast on your desktop or in a folder so you can use it every time you make edits (this takes place of step 8)
Copy and paste your new qkl_misc.ast file onto your desktop. 
Open up the AST editor and click that qkl_misc.ast file -- go to the file 7, a db file, right click, press extract selected and save the file anywhere you feel comfortable (on your desktop is fine, or make your own folder)
Open up the playbook editor exe, choose that db file, edit away and save
Open back up the qkl_misc.ast file using the AST editor, right click on the 7th file, press replace selected, and choose your edited db file; you have now replaced the default file with your custom one
Go to the editor’s top left tab and press file, save as, and replace save the qkl_misc.ast file on your USB 
Plug in your USB, choose to play the USB version and not the HDD version of the game and make sure your changes translated

FOR PC USERS
Go on the Discord and download the AST editor in #tools
Locate the game files your game plays off of. It should be a GAMES/BLUS31159-(NCAA Football 14) file with a bunch of other folders in it. Go to GAMES/BLUS31159-(NCAA Football 14)/PS3_GAME/USRDIR/qkl_misc.ast and copy the qkl_misc.ast file and paste it on your desktop
Open up the AST editor and click that qkl_misc.ast file -- go to the file 7, a db file, right click, press extract selected and save the file anywhere you feel comfortable (on your desktop is fine, or make your own folder)
Open up the playbook editor exe, choose that db file, edit away and save
Open back up the qkl_misc.ast file using the AST editor, right click on the 7th file, press replace selected, and choose your edited db file
Go to the editor’s top left tab and press file, save as, and replace save the qkl_misc.ast file in the USRDIR along with the other ast files. 
Boot up the game and make sure your changes translated
Changing Run Plays
Changing handoffs are fairly easy. Just find a run play within the same formation type (shotgun, under center, pistol), copy the QB and HB (or FB) assignments, and paste them on the play you’re changing. Note that some handoffs will not work if pasted on a play that has a different formation alignment (back directly beside QB vs back offset).  
 
Adjusting Run Blocking
In order to make the lineman block the way you want them to, you must change the hole and the play information – specifically the SRMM, SITT and PLYT. We’re not 100% clear on what SLF does but to be safe I’d copy it over anyway. We think it affects how the defense calls plays against you. SRMM is the MOST IMPORTANT to copy over, along with hole, because it affects how the lineman block. If you choose a counter play, for example, and don’t copy over the SRMM, it won’t be blocked correctly.  
 
For option plays, PLYT is necessary because it tells the play that it’s an option. If the PLYT isn’t copied over it will be a straight handoff. For any option play with a pitch man, go into the QB’s PSAL information and make sure the number in the 2nd  line of the 2nd column (under val2) matches the POSO of the pitch man. If not, create a custom PSAL of an existing QB option PSAL and put in the matching number. If not, the QB will not be able to pitch it.  
 
For all runs where the QB is the primary runner, not including QB wrap (so QB Power, Zone, Blast), the QB uses the same PSAL in all of them. The hole # determines which hole he hits and angle he runs at to start the play.  
 
Jet Sweeps / Fly Sweeps (And Fake Jet Sweeps)
Creating custom jet sweeps is finicky. Sometimes pasting a jet sweep in a new formation works, sometimes it doesn’t. Sometimes it works with the slot receiver, sometimes it works with the flanker or outside receiver. If you try to create one in the game (whether it be under center or in the shotgun/pistol) and it doesn’t work in-game, try pressing square (or X on XBOX) and LT and running the play. That essentially resets the play. Most of the time, your play will work now, but the downside is you have to remember to do this every time you run that play. This applies to any jet run that involves the QB as the primary runner (jet power, jet counter, jet inverted option, etc). NOTE: If you’re adding a jet motion onto a pre-existing run as a fake (ex. Fk Jet HB Dive), use the Pistol jet PSALS from Ace or Slot, or adjust the motion so the receiver is closer toward the line of scrimmage. This puts the receiver on a path farther toward the line of scrimmage so they won’t run into the ball carrier. 
Changing Passing Plays 
Changing passing plays is simple. You can either find a route you like in one play and copy it over to the play you want to edit (either manually or using the copy and paste tool ONLY if it’s to a player with the same POSO in both the copied and pasted location), or by clicking the PSAL and going through all the different routes to choose from within the different categories (quick out, post/corner, hitch, etc.). 
 
Adjust Play Passing Percentage
Once you’re finished with your changes, make sure you adjust the passing percentage in the play info tab. This is specifically for if you changed a previously blocking receiver to a route or the other way around. For example, if you change a HB’s PSAL from a pass block to a route and don’t change the 0 associated with the receiver to any number greater than 1, he will go on a route but you won’t be able to throw to him. Conversely, if you change a receiver previously on a route to a block and don’t change his percentage number to 0, he will have a passing icon over his head. Figuring out which number is associated with which receiver is tricky. Look in other plays within that formation and see if there are any other plays with the same eligibility layout that you’re looking for (as in if you have a play where the HB is the only ineligible receiver, find another play action pass within the formation with similar parameters and copy the numbers). The numbers themselves in the percentage don’t really matter just as long as they’re greater than 0. 
 
Making Custom Routes
Each route is made up of lines of code. You can find these codes to any PSAL by clicking on the PSAL, right clicking it, and pressing “swap/edit PSAL” or “create new PSAL”; if you’re editing, make sure to click the edit checkmark. There are plenty types of codes that create routes but these are the ones you should know about first : 
Code 8 = the route direction 
First number on the table line is distance (ex. 20 roughly means 20 steps) 
Second number on the table line is angle (mess around with this one to get a feel for it) 
Third number on the table line is speed (maxes out at 254) 
Code 9 = cuts in the route 
To make it simple, just find a cut that you like in another route and copy the code 9 and following code 8 direction
 
Code 25 = pausing or stopping for an amount of time (usually put before the first code 8 so the receiver waits to start his route or after the last code 9 to keep him stationary, like on a curl route) 
	
First number on the table line is amount of time (don’t know the exact conversion to seconds so mess around with it or look at other routes for examples, ex. Any hitch/curl routes) 
Second and third numbers are always 0 
 
Code 14 = turns route into a block and release route 
Operates just like the code 25 above – first number is amount of time he blocks, second and third are always 0 
This sounds complicated but the easiest way to learn is just to look at the anatomy of any route or PSAL.

Adding Play Action 
In order to make play action work, you must have play action PSALs for the players involved and make the PLYT 4 (and to be safe copy over the SRMM, SITT and PLF from a play action play). Keep in mind the same rules apply from run handoffs – if you put In two PSALS that are meant for one alignment (back offset, back right next to QB) and put it in another, it might not work (or look glitchy). If you put in a PLYT 4 but the QB has a standard dropback PSAL in shotgun, he will do a run fake himself (like a PA QB Power).  
 
Adjusting Pass/PA Blocking
Also keep in mind you will probably want to add some form of play action blocking for the lineman. You can either keep them in standard pass blocking, use play action blocking (884, 1888, 1) or run blocking (11, 233, 2) which operates similarly for play action. If you want to pull a guard on play action, steal the PSALS from a PA Inverted Option in shotgun. If you want your lineman to cut the defensive lineman or “crab block,” change the PSAL to 1419 and keep the ARTL at 225 and PLRR at 1. If you want your lineman to pass block while moving to the left, change the PSAL to 140, and for the right, use PSAL 699. Keep the ARTL and PLRR at whatever they are. For screens, just copy and paste from another screen play.  
QB Rollouts/Sprintouts
To add a rollout or sprintout, just steal a PSAL from another sprint play in that formation type (shotgun, pistol, under center). You do not need to change the PLYT. If you want to adjust the pass blocking specifically for a sprintout, look at any edited sprintouts and copy the lineman PSALS. 

Adding Plays to a Formation (Must Have Excel or other CSV reading application)
Note: This can get a bit confusing so follow EVERY STEP EXACTLY
Save a backup db file (just in case). Download the Madden DB Editor (it should not require a config). Open the editor, open your playbook db file, and find and export the tables SETL, PLYS and PBPL into CSV files, as well as the PLRD tab (if you’re adding run plays) or the PLPD tab (if you’re adding passing plays)
Open up the SETL file and find the formation you want to edit. Each formation is sorted by its FORM type (shotgun, singleback, etc.) Use the FORM numbers below to help find the formation you’re looking for:
1: Shotgun; 3: I formation; 4: Strong formation; 5 Split formation: 11 Ace formation: 13 Weak formation; 92: Empty; 95: flexbone; 102: Power I; 103: Pistol; 105: Wishbone; 115: Maryland; 129: Wildcat; 132: Hail Mary; 133: Wingbone
Once you find the formation you’re looking for, find the associated SETL number
Once you have that number, open up PBPL, PLYS and PLRD/PLPD (whichever one you exported). Minimize everything except PBPL.
In the PBPL, sort by SETL and find the SETL number with your formation. You should see every play with their corresponding names and information. Copy the entire row of any run play. Insert however many rows you want (if you want to add 3 run plays, insert 3 rows). Paste the row into your created row. If you’re adding pass plays, do this process but with passing plays. Just to make it easier when everything is done, rename all the pasted plays to something that indicates it’s an empty play (ex: EMPTY1, EMPTY2, EMPTY3, etc.). Also delete the PLYL number in each new row so that cell is empty. You’ll come back to that later. Locate and remember the PLYL of the original play you copied. 
Minimize PBPL and open up PLYS. Sort the entire table by PLYL. Find the PLYL of that original play (it should be a 5 digit number). There should be 11 rows with the same PLYL with POSO numbers 0-10. These are the responsibilities of all 11 players within the play. Copy all 11 rows and go to the very bottom of the Excel file. 
Find and remember the last PLYL in the final row. Paste the number in the empty space below the final row. Now rename the PLYL to one number above the final PLYL number above. For example, if the final PLYL is 15113, then paste your rows and change all the PLYLs in those rows to 15114. 
Repeat this process for however many rows you inserted in the PBPL tab (ex. You added 6 rows, do this six times -- you would have one set with 15114 PLYL, the second with 15115, the third with 15116, the fourth with 15117, the fifth with 15118, and the sixth with 15119). 
Now reopen the PBPL file and enter the new numbers into each of your new cells. So following along with the example, you’d put the first new number (ex. 15114) into the first created cell (EMPTY 1) and continue until each new row has its corresponding new PLYL.
Finally, open up the PLRD or PLPD. If it’s PLRD, sort by PLYL, go to the very bottom, insert these new numbers in the empty cells, and enter any number in the Hole column. Since you’re changing the play it doesn’t really matter what the hole number is. If you’re adding pass plays, sort by PLYL, find the PLYL of the original play you’re copying, paste the entire row, go to the bottom, paste the entire row in the empty space and change the PLYL number(s) to the new ones. 
Save all three documents. Go back to your db editor with your custom playbook file open and import each file into its corresponding tab. Save the db. 
Open back up the db into the playbook editor and make sure everything translated. If it’s a run play open up play info and make sure it has a hole number. If it’s a pass play, open up the play info and make sure it has route percentage options. 
If something went wrong, open up your backup and try again. 

Troubleshooting / FAQs
Q: There’s a glitch where the quarterback doesn’t hand the ball off to the running back when I run the play in no huddle. What causes that?
A: This is a glitch in the vanilla game. We do not yet know what causes it. Sometimes when a play is run in no-huddle, and especially when the ball carrier is motioning before the snap (e.g. jet sweep), the quarterback doesn’t hand the ball off and instead just tries to run it himself. Sometimes this can be fixed by pressing square/X + LT to reset the play but most of the time it doesn’t. It’s a glitch that has been in NCAA Football for many years.

Q: I created a custom playbook and when I go in no-huddle some of the formations disappear or show the correct formation name but a different formation itself (e.g. it says Shotgun Spread but it’s really Shotgun Wing Trips). How can I fix?
A: This is a glitch that’s in the vanilla game and has been in NCAA since 2010. There is a workaround. This glitch, at least in my experience, only affects the last four or five shotgun formations you put into your playbook and the issue is only when you have a lot of shotgun formations. So to work around it, put in the shotgun formations you care about least at the very end. I put the formations I rarely use in no huddle here. 

Q: When creating a play I get a popup that says “Unhandled Exception Error” -- what does this mean and what did I do wrong?
A: This simply means that the app can’t read what you’ve just put, whether it be an incorrect PSAL or coding within a PSAL that doesn’t make sense. It also appears when the PSAL doesn’t match the ARTL and PLRR, which is OK. I do that often when I want a PSAL to show up a certain way on the play screen. Say I want to create my own PSAL but give it an ARTL and PLRR from an existing PSAL. That’s doable, and it will translate in game, but you’ll get the unhandled exception error when you click on the ARTL or PLRR. If you accidentally put an incorrect PSAL or something in just click out and correct it.

Q: The playbook editor seems to slow down in responsiveness the longer I’m on it. Am I doing something wrong?
A: No, this is just the way the app works. Get into the habit of saving, quitting and rebooting after every 5-10 minutes. It’s also a good habit to get into because it forces you to save. 

Q: Any time I try to edit the defense I get the unhandled exception error. Why is that?
A: Unfortunately at this time we cannot edit defenses using the playbook editor so just stay out of it. It also does that on some special teams formations.

Q: I’m creating a run play but the lineman aren’t blocking the way that I want them to. Why not? They have the correct PSALS.
A: Check the SRMM, hole and PLYT in Edit Play Data and make sure they all match the type of run play you’re copying

Q: I’ve created a pass play but certain receivers have no icon so I can’t throw to them. Why not?
A: Go into Edit Play Data and make sure each number under per# (e.g. per1, per2, etc.) has a number at least 1 or above. 

Q: I have created a play and when I go into the game and I try to run the play, my game crashes. Why is this?
A: There’s something wrong with the PSALS or codes within the PSALS in that specific play, so go back and look at it under a microscope in the editor.

Q: I have created a play and one of the players just isn’t there on the field. What causes this?
A: There’s something wrong with the PSALS or codes within the PSALS in that specific play, so go back and look at it under a microscope in the editor.
