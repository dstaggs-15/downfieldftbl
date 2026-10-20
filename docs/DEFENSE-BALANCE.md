# Defense balance update

The old defense could give away the same deep touchdown over and over once you signed a few good players. Safeties stepped toward developing routes too early, and zone defenders let receivers run beyond their small patch of field. After a catch, some pursuit angles put defenders behind the runner.

Safeties now keep their depth and help against exposed receivers across their half of the field. Corners leave more cushion against faster players. Underneath defenders carry vertical releases, and a linebacker can watch QB rollouts. Defenders sprint when covering deep routes or chasing a runner. The last defender takes a more careful tackle, and long pursuit angles aim farther ahead. Running facilities give a smaller speed bonus; player speed, hands and power still matter.

The defense remembers the last ten observed plays and can call more deep coverage against repeated deep throws, or watch the QB after repeated scrambles. Its memory resets each game. Defensive calls still vary and remain hidden from the player. Big hits, hurdles, jukes, injuries and real roster substitutions keep their existing rules.

## What was measured

All comparisons use Normal difficulty, the same seeds and opponent rosters, with 80 trials per play. The upgraded offense has every starter at the listed rating, passing facilities at 3, running at 2 and an offensive coach at tier 1. Opponents use their actual league rosters. Each trial starts at the own 20. Injuries reset between independent trials.

| Repeated play | Offense | Old TDs / 80 | Updated TDs / 80 |
|---|---:|---:|---:|
| Four Verticals, same receiver, bullet after 2.25 seconds | 85 | 43 | 4 |
| Same play and timing | 95 | 64 | 9 |
| Five drawn vertical routes, choose the most open receiver | 85 | 41 | 6 |
| Same drawn play and timing | 95 | 58 | 12 |

On the updated stock vertical play, the 85 offense completed 47 throws and averaged 15.1 yards per attempt, including touchdowns. The 95 offense completed 61 and averaged 25.2 yards. On an 85-rated short-pass read, 61 of 80 throws were caught. Better players still produce more offense, and short throws remain useful.

Full-game probes repeat Four Verticals, choose the most open receiver after 2.25 seconds and sprint upfield after catches. They play PATs, halftime, turnovers, injuries and the full four-quarter clock. Injuries remain in effect within each game. There are 30 games in each row.

| Offense / kickoff choice | Old average / high | Updated average / high |
|---|---:|---:|
| 85 / normal kickoff | 19.8 / 35 | 10.5 / 21 |
| 85 / onside try after every score | 25.4 / 70 | 13.5 / 35 |
| 95 / normal kickoff | 36.2 / 49 | 21.5 / 35 |
| 95 / onside try after every score | 55.8 / 105 | 29.6 / 70 |

The updated game also passes checks for safety depth, carrying slot routes, grounded pursuit, meaningful ratings, useful custom routes and clearing learned tendencies for a new game. These scripts cover specific exploits; they do not replace human playtesting or guarantee a particular score.

Run the checks with `build/Downfield.Tests.exe --balance-test`. The full build includes them in `--test` too. Their source and complete-game driver are in `src/DefenseBalanceTests.cs`.

## Franchise and Extreme update (2.1.2026.1020)

Normal results above remain unchanged. Extreme uses faster reads of visible routes, deeper help against speed and shorter block holds. Defender ratings and base speed still come from their actual roster. In the same 80 elite custom-vertical trials, Normal produced 12 touchdowns and 33.4 yards per play; Extreme produced zero touchdowns and 13.3 yards per play. Extreme completed 43 throws in this late-read sample. In 80 early slant-read trials it completed 49 passes, with 28 first downs.

Thirty full games repeating verticals and onside attempts averaged 29.6 points on Normal and 3.3 on Extreme; Extreme's peak was 28. These scripted stationary-pocket probes do not predict every human's score. Normal still allows more room for arcade plays; Extreme requires earlier reads and movement.

CPU franchise teams now develop and recruit. In the first scripted season, 295 CPU players improved across all eleven clubs and CPU teams signed 24 of the original free agents. Eight seasons completed with legal salary caps, roster sizes, positional assignments and 36 picks per draft. Thirty-one retirements had occurred by season eight. Team offensive ratings observed across the run ranged from 75 to 88; clubs did not all become identical super teams. League budgets, staff, contracts and player potential drive those changes independently of user ratings.

The cap is now $190 and elite contract salaries are higher. Existing salaries stay intact until renewal. An isolated copy of the existing real save migrated with its user team, season/week, budget, 25 roster identities, ratings, salaries and contract years intact. The original save was unchanged by this migration probe. Model and native menu evidence are in model-test-output.txt and ui-test-output.txt.