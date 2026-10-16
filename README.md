# Downfield 2

A little arcade football game I'm working on. I wanted quick games, Madden-style buttons, and a team you can actually build up over a season. It's still a work in progress, but you can play it now.

## Just want to play?

**[Download the installer](https://github.com/dstaggs-15/downfieldftbl/raw/refs/heads/main/play%20me/Install%20Downfield.exe)**

Open it and click **INSTALL GAME**. It puts **Downfield 2.exe** on your desktop. Open that to play. The installer already has the game inside it, so there are no extra downloads or accounts to set up.

You need a **64-bit Windows 10 or Windows 11 PC**. The game uses Windows' .NET Framework. No admin account is needed. The installer isn't signed, so Windows may call it an unfamiliar app.

Or download the [Windows ZIP](https://github.com/dstaggs-15/downfieldftbl/raw/refs/heads/main/play%20me/Downfield-Windows.zip), extract it, and run the installer inside.

You can also grab the whole repo with **Code > Download ZIP**, extract it, and open the **play me** folder. Run the installer there, or skip setup and run **Downfield/Downfield.exe**.

## What the game is

You play offense and try to put points on the board. Pick a run or pass, read what's open, and use your receivers' buttons to throw. The other team's drives are simulated. Quarters are one minute, so a game doesn't take all night.

There are jukes, spins, jumps, trucks, dives, fumbles, interceptions and field goals you actually watch fly through the air. The defense changes coverages, disguises some of them and mixes in occasional two-man stunts. Bigger hits can knock someone down or cause an injury.

It's meant to be fun and arcadey. It isn't trying to copy every NFL rule or be a full Madden replacement.

![On the field](docs/gameplay.png)

## Build your team

The Clubhouse is your little pixel-art office. From there you can:

- Look through your roster and choose who starts.
- Sign players from 70 to 99 OVR.
- Trade with the other clubs.
- Renew contracts, release reserves and work around the salary cap.
- Hire offensive, defensive and conditioning coaches.
- Train players and upgrade facilities.
- Change your mascot and colors, or move the team to a different city.
- Check injuries, scout other rosters, follow the standings and see the playoff picture.

New signings start on the bench. Go to **View Roster**, select the player, and choose his starting spot.

The league has **12 teams**, including **New Orleans**. You play everyone once over 11 weeks. The top six make the playoffs, the top two get a bye, and the winners work their way to the championship. You can keep going into another season.

![The clubhouse](docs/office.png)

## Xbox controls

| What you want to do | Button |
|---|---|
| Move | Left stick |
| Sprint | RT |
| Snap | A |
| Throw | A, X, Y, B or RB over the receiver |
| Lob / bullet | Tap the receiver button / hold it and release |
| Lead a pass | Right stick |
| Juke with the ball | Right stick left or right |
| Truck | Right stick up |
| Spin | Right stick down or B |
| Jump once you're past the line | Y |
| Dive | X |
| Handoff / backward pitch | LB |
| Playbook before the snap | Y |
| Pause | Menu |

Y changes with what you're doing: playbook before the snap, receiver button while passing, jump when you're carrying past the line. Right-stick up trucks. It doesn't jump anymore.

In menus, use the D-pad or left stick, A to select and B to go back. LB/RB jumps a page in long lists.

## Keyboard controls

WASD moves, Shift sprints and Space snaps. Numbers 1–5 throw to the matching receiver. Tap for a lob or hold and release for a bullet.

With the ball, left/right arrows juke, up trucks and down spins. J jumps, F dives, E also spins and Q pitches backward. Y opens the playbook before the snap. Escape pauses.

Menus use arrows, Enter and Escape. Q/R jumps a page.

## Want to sim?

- **Schedule > Sim Next Game** skips your next fixture and advances the league.
- **Pause > Sim Rest of Game** finishes the game you're already in.
- **Playbook > Simulate Drive** finishes your possession before the snap.

The results count toward the season. Your team saves automatically, but a live game can't be resumed after closing it. Leaving an unfinished league game counts as a 0–21 forfeit.

The [full guide](docs/FRANCHISE-GUIDE.md) covers trades, contracts, training, injuries, playoff rules and saves.

## How it's made, without the computer-science lecture

It's written in **C#** and runs as a normal Windows app. It doesn't use Unity or Unreal.

The code moves the players, checks catches and tackles, keeps score, and decides what the defense does. Another part draws the field and the little pixel players. The office and menus are drawn the same way.

The art is made by the game from colored shapes. The football, uniforms and menus don't need a separate pile of image files, which is why the game can run from one exe.

The team and season live in a save file on your PC. They aren't stored on a server. The installer copies the game and instructions into your Windows user folder and puts a working exe on your desktop.

If you're looking around the source:

- **Simulation.cs, Mechanics.cs:** the football rules, movement and game flow.
- **Defense.cs, LinePlay.cs:** coverage, pursuit, blocking and stunts.
- **Impacts.cs:** trucks, knockback and falling.
- **Franchise.cs:** teams, players, trades, contracts, injuries and the season.
- **GameForm.cs, FranchiseUI.cs:** what you see and how the menus work.
- **Input.cs:** keyboard and Xbox input.
- **PixelArt.cs:** the pixel players and lettering.
- **installer/Installer.cs:** the setup program.

## If you want to build it yourself

On Windows, open PowerShell in the repo folder and run:

    powershell -ExecutionPolicy Bypass -File .\build.ps1

That makes the game in **build/Downfield.exe** and runs the game tests. To check the menus too:

    .\build\Downfield.Tests.exe --ui-test

To rebuild the installer after building the game:

    powershell -ExecutionPolicy Bypass -File .\build-installer.ps1

The installer script also tests a fresh install and reinstall in a temporary folder.

## A few things to know

This is single-player and offense-only right now. There's no playable defense, online multiplayer or save-in-the-middle-of-a-game feature. A tied playoff game uses simulated overtime.

The automated tests cover the game rules, league, saves, menus and the installer. That doesn't mean every controller or every possible play has been tested by a person. If something feels off, [open an issue](https://github.com/dstaggs-15/downfieldftbl/issues) and tell me what happened.

The source and original artwork are under the [MIT license](LICENSE.txt). Retro Bowl is a separate game; its code and assets aren't included here.
