# Downfield 2 - Player guide

## Playbook, menus and Bowling update (2.3)

Main menu: Exhibition, Franchise, Bowling, Options.

Franchise starts in the pixel office. Team contains Roster, Transfers, Staff + Training, Money + Stadium and Team Identity. Transfers contains Sign Players, Trade Players and Draft. Staff contains Coaches, Training and Facilities. Team Identity contains Edit Team and Move Team. League contains Schedule, Standings, Playoffs, Stat Leaders, News and History. Coach contains My Coach, Trophies, Champions and Coaching Jobs. Back returns to the parent section. Roster has an Injury Report button below the cards.

There are 48 stock calls. The original 24 keep their order. New calls include Flea Flicker, HB Pass, WR Reverse, Double Reverse, Statue Liberty, Wildcat Power/Pass/Option/Counter/Sweep/Flea, Philly Special, Hook + Lateral, Shovel Pass, Reverse Pass, Fake Jet Screen, Triple Option, QB Leak, TE Tunnel, Slot Screen, RB Toss Pass, Jet Pass, Slot Boomerang and Fake Double Give. Some calls run automatic backward exchanges; others let you choose give, keep, pitch or pass. On a backward give, the HB/WR can pass only on the designated trick calls while the ball has never crossed the line and no forward pass has been used. Non-QB passing has lower accuracy and arm strength. LB is still a manual backward lateral. Normal football's one-forward-pass limit remains.

Gold play art shows the carry lane; colored lines show receiver routes, sampled from gameplay. Block bars mark run blocking. LB/RB changes playbook pages. In Create Play, drag from a receiver or pick a preset, then Save to Book. Name Play opens a keyboard or controller letter grid. Up to 40 named plays save with the franchise, appear after stock calls, and also work in Exhibition/Bowling. My Saved Plays lets you call, edit, rename or delete them. Edits overwrite that play's slot; a new draft gets a new slot. Old one-play saves migrate into the library.

When no healthy QB remains, the game opens an emergency QB chooser before the next snap. Choose any healthy roster player, including reserves or defenders. He keeps his own identity and leaves his old offensive spot empty if needed. He can throw, with substantially lower accuracy and ball speed. The choice persists until unavailable. Pause > Emergency QB lets you change that choice during the shortage. Wildcat calls put the real RB under center and use a real backup for the give option.

For an aggressive catch, press Y while a forward pass is in the air. A moving meter appears above the intended receiver. Press Y again to lock it: green 100%, yellow 75%, red 4%. An activated but unfinished meter counts as red. Without the first press, normal speed/hands/coverage catching remains. Keyboard default is Y. Receiver-button Y while preparing a throw remains a pass button; runner Y remains jump. A green release guarantees the intended aggressive catch, so timing is a strong arcade tool.

Options > Controls and Pause > Controls offer separate keyboard/controller mappings. Select an action, then press the new key/button/trigger. Conflicts in the same context swap. Controller face buttons retain shared Madden-style contexts, and movement/skill sticks may swap. Enter/Escape remain menu recovery keys; F11 is reserved for fullscreen. Reset Defaults restores both devices. How to Play displays current bindings. A connected physical Xbox controller was not available for feel testing; translation is covered by model checks.

Cross-position trade targets now include every role. Review compares the outgoing offer with what the CPU needs: OVR, age, potential, position value, injury, years, salary, replacements and contender/rebuild plan. The CPU refuses large player-value gaps even with abundant cash. Fees do not pay you money back. Both teams must stay under the cap. A cross-position move fills matching vacancies or promotes a real reserve; it never turns an RB into a starting QB by swapping slot numbers.

Regulation games have three timeouts in each half, reset at halftime. Pause > Timeout or the playbook Timeout calls one between plays; the scoreboard shows how many remain. Bowling has no clock or timeouts.

Bowling is a separate quick mode. Choose both teams, weather, and CPU or local two-player shared-controller play. Every frame starts ten yards out. Gain four yards and play two starts six yards out. A first-play touchdown is a strike; a second-play touchdown is a spare. Incomplete passes and turnovers use one attempt for zero pins. Whole positive yards count in open frames. Teams alternate after each frame. A strike adds the next two plays' pins, a spare the next play's pins; the tenth awards the required extra attempts. Twelve consecutive strikes score 300. The scorecard leaves a total pending until its bonus is known. No kicks, PATs, onside attempts or regulation sim drives. The CPU runs actual on-field offensive plays. Local play asks the other player to take the controls. Franchise records and money are untouched.

The research references are in [MENU-AND-BOWLING-RESEARCH.md](MENU-AND-BOWLING-RESEARCH.md). The older sections below describe the features retained from earlier releases.

Open **Downfield 2.exe** on the Desktop. **Downfield 2 Updated.exe** is also refreshed with the same build. The source and rebuild script remain in `Downloads/Downfield-v2`.

## Start here

1. Select **Franchise**, then **Team**. Your team colors, pennant, play board, desk and GM appear in an original pixel-art office.
2. **Coaching Jobs** lets you take charge of one of 12 clubs before the season opener or between seasons. New Orleans Crescent is included. This selects an existing franchise; **Move Team** instead relocates your current club while keeping its roster, records and schedule.
3. Open **Roster**. Select a player to set his starting slot, renew his contract, release him, or seek a trade. Newly signed players arrive as reserves; you choose where to start them.
4. **Schedule** has Play Scheduled Game and Sim Next Game below your fixtures. Use LB/RB to jump a page, or Q/R on keyboard. Advance League handles byes and spectator playoff rounds.

Office navigation: D-pad/left stick moves through departments; left/right changes columns. A enters and B returns. On keyboard use arrows, Enter and Escape. Long lists scroll through pages and show a row counter. Transaction reviews start on Cancel; choose Confirm to commit a purchase, trade, relocation or staff hire.

## Team building

The initial squad has 25 distinct players: 22 starting assignments plus reserve QB, RB and WR. The limit is 30. Your starting 11 offensive players are individually represented on the field, and your opponent fields its own defensive 11. Your defensive roster contributes to simulated opponent possessions. Signing a WR or lineman no longer upgrades everyone at that position.

Players have OVR, speed, hands, power, age, salary, contract length, morale, development XP and injury recovery time. OVR influences acceleration/reaction/release, speed determines pace, hands influence receiving and power affects throwing/blocking/trucking. Skills can develop up to 99.

- **Sign Players:** 30 initial free agents span every OVR from 70 through 99. Review the signing fee and salary before buying. Free agents join on three-year contracts and use an individual reserve slot.
- **Salary cap:** A $190 payroll cap. Elite salaries rise sharply: 80 OVR costs $7, 90 costs $12, and 99 costs $20. Salary occupies cap space separately from the signing fee. Releasing a reserve frees cap space without refunding fees. A starter must be replaced before release. Existing contracts keep their salary until renewal; an over-cap save keeps its players but must make room before adding or renewing contracts.
- **Depth chart:** choose an eligible starting slot for a player. The former starter moves into that player's previous slot or onto the bench. This allows different WR and OL assignments instead of replacing an entire position group.
- **Contracts:** renewals add two seasons, capped at four, and update salary to the player's current rating. Contracts age at the start of each new season. Expiring players enter free agency. A healthy reserve fills a vacancy; otherwise the club gets a 70 OVR replacement before the draft.
- **Trades:** Team > Transfers > Trade Players allows any outgoing/incoming positions. The review checks player value, role needs, age, potential, injuries, contracts and both salary caps. Large value gaps are rejected. Trading closes in Week 9.
- **Coaches:** hire tiers 1–3 in offense, defense and conditioning. Offense improves the passing unit. Defense improves opponent-drive outcomes. Conditioning reduces stamina use and injury risk and adds training XP. Hiring replaces the tier in that staff role; it does not stack infinite coaches.
- **Training:** one $3 session per league week, targeting passing, running or defense. Under-26 players need eight XP to develop; veterans need 12. Games also award XP. Ratings are capped at 99, and veterans over 30 lose a little speed and OVR each offseason.
- **Facilities:** upgrade passing, running and defense facilities to level five. Costs rise with each level. Facility and staff changes apply to the next match.
- **Move Team:** costs $15 and requires an unoccupied city. Moving preserves your league identity and standings record.
- **Edit Team:** cycle mascot, primary color and secondary color, then save. Branding changes are free and update the office, main menu, scoreboard, uniforms and end zones. Identical primary/secondary colors are adjusted to keep a contrasting accent.
- **Standings:** inspect wins, losses, ties, points scored, points allowed, differential and team ratings. Select a club to scout its roster.
- **Injury Report:** shows unavailable players and weeks until recovery. The roster also marks injured players.

Cash is shown with a dollar sign. Each game pays league/result income and, at home, stadium sales. Payroll, staffing, facilities and venue operation produce bills. Byes and spectator weeks still charge running costs. Championship and new-season grants are $8 each. Sim games use the same finances. Existing balances and upgrades are preserved.

## League and playoffs

There are 12 fictional clubs: Chicago, New Orleans, Atlanta, Dallas, Miami, Seattle, New York, Los Angeles, Denver, Buffalo, Portland and Nashville.

Each club plays all 11 other clubs once. Your match finishes the week, and the other five fixtures are simulated. Standings rank by wins/ties (two points per win, one per tie), then point difference, then points scored, then stable team order. Regular-season games may end in a draw.

The top six qualify. Seeds 1 and 2 receive first-round byes. Wild cards are 3 versus 6 and 4 versus 5. The semifinals reseed: seed 1 plays the lowest surviving seed and seed 2 plays the other winner. The two semifinal winners play for the championship. The projected bracket is explicitly provisional until Week 11 ends.

If you are on a bye or eliminated, Advance Round resolves the remaining games. After the championship, begin the next season from Schedule. New seasons refresh the free-agent class, age players/contracts, retain club investments and career titles, and create a new schedule. A tied playoff game gets a **simulated sudden-death field-goal overtime**, weighted by team strength; playable overtime is not implemented.

## Gameplay and controls

Quarters are **60 seconds**. The offense-first arcade format remains: you play offense, while opponent possessions are simulated. There is no user-controlled defense or multiplayer.

| Action | Xbox | Keyboard |
|---|---|---|
| Move / sprint | Left stick / RT | WASD / Shift |
| Snap | A | Space / Enter |
| Receiver icons | A, X, Y, B, RB | 1, 2, 3, 4, 5 |
| Lob / bullet | Tap / hold-release receiver icon | Tap / hold-release number |
| Small placement at release | Left stick | WASD |
| Extra manual lead | Right stick | Arrows |
| Throw away behind the line | Right-stick click | T |
| Quick mute | View button / sound button | M / sound button |
| Juke left / right while carrying | Right stick left / right | Left / right arrows |
| Truck while carrying | Right stick up | Up arrow |
| Spin while carrying | Right stick down or B | Down arrow or E |
| Jump past the line of scrimmage | **Y** | **J** |
| Dive | X | F |
| Handoff / backward lateral | LB | Q |
| Playbook before snap | Y | Y |
| Cycle pre-snap calls | D-pad left/right | Left/right arrows |
| Pause | Menu | Escape / P |

Y remains a passing icon while the QB can throw and opens the playbook before the snap. When carrying past the line, Y jumps; right-stick up now trucks instead of jumping. Skill moves cost stamina, have visible cooldowns and require the right stick to return toward center between flicks.

The playbook has 12 passing packages and 12 runs. New passes include mesh cross, curl flats, HB wheel, corner shot, play action and Texas angle. Runs include dive, inside zone, power right, counter left, sweep right, stretch left, HB draw, off tackle, toss, jet sweep, read option and QB draw. Runs vary lane, blocking priority and draw mesh timing; steer after the handoff.

## Exhibition and halftime

The boot screen separates **Exhibition** from **Franchise**. Exhibition lets you pick both teams, start a quick game or practice. Its squads, injuries and results do not change your saved team, schedule or coach career. Franchise owns the clubhouse, league and team-building systems.

You receive to start the game. At halftime there is a stop screen, a new 60-second third quarter, three timeouts, first-and-ten at the 20 and an opponent receiving possession. A touchdown as the second quarter expires still gets its conversion before halftime. First-to-second and third-to-fourth quarter changes keep the current drive.

Regular games have team field art. Postseason scoreboards say PLAYOFF GAME or CHAMPIONSHIP GAME. Midfield reads DOWNFIELD PLAYOFF BOWL or DOWNFIELD CHAMPIONSHIP BOWL.

## Coach, history and trophies

**Franchise > My Coach** edits your name, skin tone, hair, hat and outfit. The letter picker works with Xbox buttons, mouse or typing. Names allow 16 characters. The same screen shows career wins, losses, ties, win percentage, playoff wins and titles.

**Coach > Coaching Jobs** changes clubs before the first game or after the championship. Your coach appearance, name and record follow you. Relocating with **Move** keeps your current team instead. Old saves recover results still present in their fixture list and retain known titles; missing past seasons are not invented.

**History** archives all 12 clubs after each championship: season, record, points and champion. Select an entry for that club's history. **Champions** lists each season's winner. These records survive new seasons and relocation.

**Trophies** has pixel championship, MVP and OPOTY trophies. Your coach keeps trophies earned with his club. Each category opens the league's winners list. Awards arrive after the championship, once per season. MVP uses production plus a small team-win bonus; OPOTY uses offensive production alone. Passing yards, rushing/receiving yards, touchdowns, catches and interceptions contribute. Defensive MVP statistics are not modeled. **Stat Leaders** has MVP, Offensive Player of the Year, passing, rushing and receiving tabs. It shows portraits, teams and actual totals. Use left/right or LB/RB to change category and up/down to compare players. The visible award ranking uses the same calculation as the final trophy. Played games use player box stats; games with simulated action use deterministic score/ratings estimates for the award calculation. Exhibition never contributes.

## New play tools

The playbook shows six visual cards per page. Read Option starts with the QB: **LB / Q gives** to the back before crossing the line, or keep and run. QB Draw stays with the QB. Jet Sweep hands to the slot receiver. Receivers pursue blocks once your carrier crosses the line, including QB scrambles.

For a small placement adjustment, move **left stick / WASD as you release** the receiver button. Maximum offset is 1.75 yards sideways and 2 yards up/downfield. Neutral means the normal predicted receiver location. QB power, accuracy, movement and pressure still affect the throw. The existing right-stick/arrow manual lead remains available.

Open **Playbook > Create Play** before the snap. It starts with five visible routes.

1. Pick a receiver on the left.
2. Choose a route on the right: go, slant, out, in, cross, curl, post, corner or flat. The preview changes immediately.
3. Press **Use Play**, then snap on the field.

**Stock** restores that receiver's original playbook route. **Save to Book** stores a named call in the custom playbook. My Saved Plays opens the library; LB in the designer restores the last saved draft. A short message confirms each action. Empty saves cannot overwrite your play. Calling a stock play exits the custom play.

With Xbox: D-pad/left stick navigates, A selects the receiver or route, Y changes receiver, RB saves, LB loads, and Menu uses the play. Move right from routes or down from the last row to reach the bottom buttons. B returns to player selection, then exits. With keyboard use arrows/WASD, Enter, Y, R, Q, P and Escape.

Drag directly from a receiver to draw a route, or select **Draw Route** and drag on the field. A click without a drag keeps the current route. The first drawn point replaces the preset; each receiver allows 64 points. With Xbox, **X** opens drawing mode; move the left stick and hold **A** to draw, release A to move the cursor without drawing, X undoes and B finishes. Mouse users can also use Undo. Routes are relative to the line of scrimmage. Curl, in, out, cross and flat routes stop at their endpoint until scramble behavior takes over. The editor changes receiver routes, not formation or run blocking.
## Sounds and ratings

Stadium ambience runs during matches, with louder cheers and a horn for scores, applause for first downs, big-hit reactions and groans for turnovers or missed kicks. Hits, soft play-end markers and crowd sounds play together. Mute and losing window focus silence all layers. Crowds use real CC0 field recordings; the contact, horn and UI effects remain original synthesis. Everything is embedded in the EXE. See docs/SOUND-NOTES.md in the repository.

Speed now has a wider range: a 99-speed WR is about 3.3 yards/second faster than a 70-speed corner before situational modifiers. OVR changes acceleration and defensive reaction, hands changes drops, and power changes accuracy/contact. A matched 200-throw open-receiver probe caught 147 with 70 hands and 185 with 99 hands. This is a scripted sample, not a promise that a covered receiver always wins.

## Arcade timing, injuries and sound settings

Hurdles last about half a second. Press Y just before meeting the defender so the collision happens during the middle of the jump. A well-timed hurdle can clear corners and linebackers as well as linemen. Ratings and nearby support still matter. Colliding at takeoff or landing can cause a harder hit, a fumble or a longer injury. Jumping in empty space does not randomly injure you.

Jukes travel a little farther and defenders sometimes commit to a tackle they cannot steer. Runs shed blocks sooner, and free defenders take interception angles and work around the blocker they just shed. These changes keep offensive skill moves useful while giving the defense a chance to make a stop.

When sprinting straight upfield well past the line with every available defender behind and at least nine yards away, the whole play gradually runs up to 1.45 times faster. The clock still uses real time. Turning, using a skill move or approaching a defender returns normal pace immediately. It is a short finish for an uncontested breakaway; speed ratings and distances stay intact.

Four seconds are deducted once at the end of each timed in-bounds play, including a touchdown or field goal. Incompletions, throwaways and out-of-bounds plays get no runoff. PATs and two-point conversions are untimed and do not charge another runoff. If runoff ends a quarter or half, the normal quarter/halftime rules apply.

Bigger injuries remove a player immediately. Ordinary contact injuries last two to five weeks; hard landings can last three to six. A pixel ambulance arrives after the whistle, medics load the patient and the vehicle leaves. The camera follows the pickup, and the clock stops during it. Press A/Enter to skip the pickup, then continue to the next play. A real healthy reserve takes over with his own name and jersey. If nobody is available, the spot stays empty. The health page and player card show the recovery time and injury type.

Open **Pause > Audio Mixer** for Master, Crowd, Whistles, Hits + Moves, Stadium Horns and Menu Sounds. Left/right changes a selected volume; A mutes that channel. Mouse clicks and dragging work on the bars. Mute All silences everything. The sound button in the field view and Xbox View / keyboard M are quick mute controls. Settings save separately from the franchise and survive reinstalling.

Right-stick click / T throws the ball out of bounds while the QB can still pass. It cancels a charged throw, shows the ball in flight, counts as an incomplete attempt and stops the clock without runoff. It is unavailable after crossing the line or after already throwing a forward pass.

## Grounded movement and arcade contact

Movement accelerates toward the requested speed, plants on release, and slows into sharp reversals instead of instantly changing direction. Running backward is slower. Animation follows distance traveled, with visible body bob, cuts and planted feet. Nearby teammates separate instead of occupying the same location.

Jukes create actual lateral space instead of granting blanket tackle immunity. Trucks use power versus defender power and nearby support, and can fail. Spin can beat an isolated reach but is not guaranteed. Jumps visibly lift the carrier and may clear a low lineman tackle; they do not make the runner untouchable. Big contacts produce a hit burst and reaction. Winning a truck launches the defender backward into a tumbling knockdown; losing a truck knocks the carrier back hard. Falls last about a second and continue after the whistle without changing the dead-ball spot. Contact fumbles remain possible, especially while sprinting.

Injuries can occur on big hits. Injured players sit out future plays and are replaced by a healthy reserve at the same position, with his own jersey. If no healthy reserve is available, you play short-handed; no emergency player appears. An existing healthy player can move to wildcat QB if every QB is injured. Recovery is two to six league weeks; a week advances even during a bye. Conditioning reduces injury risk. Simulated games can cause injuries too. There are no medical expenses, real-world injury diagnoses or permanent career-ending injuries.

## Defense and receiver separation

Eight defensive calls: man free, cover two, cover three, zone blitz, press man, quarters, Tampa two and robber. Many use a disguised two-high shell until after the snap. The game does **not** display the defensive call.

The four rush plans are straight rush, tackle-end loop, end-tackle loop and cross fire. Straight rushes are the default (about 80% in a sampled sequence). Only one two-man pair stunts, stunt sides vary, and there are no back-to-back stunt calls. Loopers reach landmarks behind their partner before turning toward the quarterback. Blockers engage one rusher at a time; physical contacts and timed sheds replace scripted shoves. Edge rushers hold outside leverage against retreating quarterbacks. Linebackers/corners take interception angles and outside leverage on runners; safeties close lanes instead of forming a straight pursuit queue. Repeated offensive calls speed up defensive recognition a little.

Man coverage reacts on an interval influenced by OVR. Safeties hold depth and help on exposed routes, while corners allow more cushion against faster receivers. Zone players protect areas and carry vertical releases beyond their initial landmark. A linebacker can watch rollouts and repeated QB scrambles. Defenders sprint during deep coverage and pursuit; the final defender takes a more careful tackle. The defense learns from its last ten observed plays, varies its coverage choices and clears that memory each game. Running facilities add a smaller speed bonus. Press releases depend on relative ratings/power. Individual speed differences, acceleration and route breaks create separation. The defense reacts to a thrown football after a delay instead of instantly knowing its destination.

Receivers continue moving with scramble routes after their initial route develops, or when the QB moves far outside/behind the pocket. Comebacks, lateral outlets and differing depths replace the old static endpoints. Passing still uses spatial catches, drops and interceptions; a covered target is not guaranteed to catch.

These are compact arcade models, not a complete NFL coverage or collision simulator.

## Field goals and field dimensions

Stop the kick meter, then watch a kicker windup/leg pose and a visible ball flight. Points are awarded only after the flight. Inaccurate kicks travel outside the uprights; out-of-range kicks fall short. PATs use the same visible kick sequence.

The field is 100 yards between goal lines, 53 1/3 yards wide, with a 10-yard end zone at each end. Goal crossbars are at the back end line, with an offset support behind it; the crossbar is 10 feet high and uprights are 18 feet 6 inches apart. Extra painted turf beyond the end zone has been removed. Original pixel rendering remains stylized.

Dimension reference: [NFL Football Operations — The Field](https://operations.nfl.com/rules-officiating/2026-nfl-rulebook).

## Simulation

- **Next game:** League > Schedule → Sim Next Game. A review explains that your fixture and the other clubs' games count toward the season.
- **Current drive:** use Simulate Drive in the pre-snap playbook or pause menu. This resolves the whole offensive possession using ratings and field position, including a simulated conversion after a touchdown. It is unavailable in practice, during a live play or during a conversion.
- **Rest of game:** pause → Sim Rest of Game. The current score is retained and remaining possessions are simulated. The result is committed once to the league. This also handles a pending kick as part of the simulated remainder.

Simulated drives are logged as simulated summaries; they are not reconstructed as individually played passes, rushes or kicking animations. Match statistics mix played action with the explicitly simulated drive/score summaries. The current match is not resumable after exiting. Leaving an unfinished league match records a 0–21 forfeit; the leave menu tells you this before confirmation. Window close also records that forfeit.

## Saves, backup and validation

New franchise state is saved beside the old club file at `%LOCALAPPDATA%/Downfield/club.txt.franchise.xml`, with an atomic previous-version `.bak`. Existing `club.txt` and `club.txt.roster` supply migration data and remain present. The original source/build before this update is backed up in `Downloads/Downfield-v2-backup-before-franchise`.

**833 automated checks pass:** 634 model/audio/settings checks and 199 native menu/input checks. These include eight seasons of AI development, drafts, retirements, cap and lineup checks, plus Extreme and Normal gameplay comparisons. Tests use isolated save directories. Screenshots show the office, full roster, draft, market, management pages, bracket and gameplay.

Balance probes: 64 catches in 120 varied defended throw scenarios; 39 catches produced more than two yards after catch. A fixed sideline-sprint pattern was stopped in 60/60 trials, with about 2.9 yards average gain. Upgraded-roster and full-game comparisons are in [DEFENSE-BALANCE.md](DEFENSE-BALANCE.md). These are scripted scenarios, not a human playtest or a guarantee against all exploits. Physical Xbox controller feel and prolonged human balance testing remain unverified.

Rebuild: `powershell -ExecutionPolicy Bypass -File .\build.ps1`. Then run `build\Downfield.Tests.exe --ui-test` for native UI checks. No new runtime dependencies or proprietary Retro Bowl code/art are included.

## Onside alternative

After a PAT, two-point try or made field goal, choose a normal kickoff with A/Enter or an onside try with X. Before any normal offensive snap, RB / keyboard R or Playbook > Onside Try also offers it. There is no quarter, score or attempt limit.

It is one normal live play of fourth-and-13 from your own 30. Reach your own 43 to keep possession with first-and-10 at the finishing spot. A short gain, incompletion, lost fumble or interception gives the opponent possession under the normal turnover rules. Incompletions stop the clock; in-bounds plays have the normal runoff. You cannot punt, kick a field goal or simulate an entire drive during the try. The first attempt each game shows a diagram and explanation before play begins. Halftime/final clock rules still apply.

## Flea-flickers and roster identity

After a handoff, use LB / keyboard Q to pitch backward toward the QB. A caught return behind the line gives the QB normal receiver-icon passing controls, and receivers resume pass routes. A forward pass is legal only if the ball has never crossed the line during that play and no forward pass has already been thrown. Returning behind the line after crossing it does not reset that rule. Pitches can still be dropped or recovered by the defense.

Every field player uses an actual roster ID, name and jersey. Reserves keep their own numbers, and duplicate jersey numbers within a club are resolved. Injured players stay out until they recover. With no healthy replacement at a position, that spot is inactive: it cannot catch, block, cover or appear on the field. A short-handed run without an RB becomes a QB keep. If all QBs are hurt, a healthy player already in the lineup moves to wildcat, leaving his former spot empty. The game never clones him into two positions.

The strongest trucks and closing collisions can launch a player several yards, with a brief camera kick, turf burst, tumbling fall and dazed stars. These animations preserve the actual dead-ball spot. Big-hit and hard-landing injuries are a little more common; conditioning still reduces the risk. The ambulance carries off the injured person, then the next play uses the remaining roster.

## Stadium money, fans and your job

Open Clubhouse > Stadium + Money. Food changes by $1 and parking by $2. Food ranges from $0 to $20; parking from $0 to $40. The screen shows fan happiness, job security, expected attendance, projected sales and the last game's income and costs.

Higher prices can improve initial sales but reduce demand and fan support at home games. Cheap prices help fan mood while earning less per sale. Results also change support. Winning helps the board's confidence; losses, angry fans and debt reduce it. Bills include payroll, staff, facility upkeep and extra home-game costs. Operating expenses can put the budget below zero and prevent new purchases.

The board begins firing reviews after at least four games in the job. Very low job security, three consecutive reviews with fan support below 25, or debt of $25 can end the job. Your name, appearance, wins, losses, playoff wins and championships stay. Coaching Jobs marks openings at struggling clubs; a fired coach can accept one midseason. Each club keeps its own money and fan support.

## Developing league, draft and difficulty

CPU clubs now earn game XP, develop toward individual potential, improve staff, sign from the same market, renew contracts, and sometimes trade an older contender-ready player for a younger prospect. Their money and $190 cap are tracked independently. Losing clubs can reset their coaches and rebuild; successful clubs try to keep good veterans. There is no automatic scaling to the user's rating. Standings show each CPU team's plan. League News carries transactions and retirements.

After the championship, Schedule > Begin Next Season starts the offseason and opens Draft. All 12 teams pick once per round for three rounds. The worst regular-season record goes first, the champion last, and the order repeats each round. You select three rookies across the draft; CPU picks run automatically between yours. Each rookie shows OVR, potential, age, skills and salary. Potential is a development ceiling for CPU training, not an immediate rating boost. Rookie contracts last three seasons with no signing fee. Auto Draft lets your scouts finish all remaining choices.

Draft selections and the board save between picks. Kickoff waits until the draft ends. At the roster/cap limit, drafting waives the lowest-rated reserves needed to make room, with names recorded in News. If reserves cannot create enough space, return to Roster and make cap room before choosing that player. A draft does not automatically start your rookies: use Roster > Set Starter. Draft Room shows your completed class and recent retirements.

Age now matters: players lose some OVR/speed after 29, with faster decline after 33. Retirement can begin at 32 and is certain by 38. Retirements retain player identity, team and season in history data. Healthy offseason replacements fill vacant assignments; an injury during a season still requires an actual healthy reserve, or the team plays short.

Roster, Trade, Free Agents and opponent scouting use six-column player cards, with up to 30 visible at once. Gold ratings are 90+, green 80+. The selected footer shows the player's exact skills, age and contract. LB/RB or Q/R changes pages when needed. Standings show all 12 clubs on one screen. Staff, training, facilities and player actions use larger cards.

Options and Pause offer Rookie, Normal, Hard and Extreme. Extreme reads visible routes faster, keeps deeper help against speed, closes passing windows and shortens blocking holds. Tacklers commit later, and linebackers react to escaping QBs sooner. Real roster speed and ratings are preserved. Difficulty saves separately from franchise results. Normal's existing gameplay is unchanged. In 30 scripted elite-roster games repeating deep throws and onside attempts, Normal averaged 29.6 points; Extreme averaged 3.3. A human who mixes reads and moves can perform differently.
## SPRXTRM, receiver commands, weather and team identity (2.2)

Options and Pause now have five difficulties: Rookie, Normal, Hard, Extreme and SPRXTRM. Extreme has faster breaks on the ball, tighter help, better cutback support and quicker block shedding. It no longer leaves short receivers free while linebackers blitz. Normal retains its defensive tuning.

SPRXTRM is deliberately unfair: all on-field defenders have 99 OVR/hands/power and a fixed speed advantage, very short block holds, immediate coverage reactions and huge contact hits. These are temporary mode bonuses, not permanent roster improvements. The line still moves around physical blockers; it does not teleport through them. Changing difficulty refreshes player bonuses at the next snap. Real contact is required for the hit animation. This is the mode to choose when you want very little room for error.

To direct a receiver while your QB can still pass, hold LT on Xbox. The nearest receiver is selected first; tap A/X/Y/B/RB to choose another, then flick the right stick left/right/up/down to send him across, deep, or back toward the ball. Center the stick before another command. On keyboard, hold C, select 1-5 and use arrows. Commands last up to four seconds and the pass forecast follows the commanded route. Release LT/C to throw normally. The modifier cancels a held pass so it cannot accidentally fire. Right-stick click/T still throws away; after crossing the line, right-stick moves remain jukes/trucks/spins.

Franchise forecasts use the actual home city and league week. September progresses toward winter as the season advances; cold cities can get snow and wetter cities more rain. New Orleans, Dallas, Atlanta and Los Angeles use covered stadiums in Auto. Custom cities use the default temperate climate. Forecasts are simulated, not live weather. Exhibition has an Auto/Clear/Rain/Snow card; forcing rain/snow overrides the roof. Weather changes the field and precipitation, adds modest footing/catching/kicking effects, and appears in the venue strip. The opening broadcast card dismisses as soon as you snap.

Every mascot has original pixel artwork. Logos appear in team selection, franchise headers, scouting/standings/history and midfield. The field and venue weather use the actual home team, even when your franchise is away. Mascot/color changes update the logo. Six contact animations vary flips, flat-back clotheslines, corkscrews, bounces and cartwheels. They share the same grounded knockback rules.

History opens a list of completed seasons, one card per year. Select a year for its champion, MVP/OPOTY and all 12 final records. Select a club for its seasons, points for/against and playoff finishes. Records shown are regular-season W/L/T; championship status is separate. New archives store that year's logo/colors and name. Older saves retain their names and use the current logo when no past branding was saved.

Move Team includes Type a Custom City. Enter 2-24 letters/numbers/spaces/hyphens with the keyboard or controller letter grid and review before paying. Cost: $45M total, made from the existing $15M move plus $30M extra for a custom city. Players, standings and coach history stay with the club. Existing cities must be unoccupied. Front-office funds/payroll use league millions; food and parking prices are per-customer dollars.

Validation: 833 automated checks pass (634 model/audio/settings and 199 native UI checks). The expanded hard-mode sample covers adaptive QB draws, evasive outside runs, rollout slants and rollout mesh throws with a 95-rated offense. In 240 trials, the previous Extreme allowed five touchdowns; this release allowed one, with average yards across the four attacks falling from about 6.1 to 3.2. SPRXTRM allowed zero in those trials and penetrated/sacked 60 of 60 stationary 99-rated pockets within three seconds. Early throws still work on Extreme. These are scripted probes; human play can find different results. Native Xbox hardware feel remains unverified in this session.