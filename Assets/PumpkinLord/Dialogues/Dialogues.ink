EXTERNAL MoveTo(boxType)
EXTERNAL StartBattle(enemyId)
EXTERNAL StartNextTurn()

===HorseDialogue===
#horse_l1
Where do you want to travel? (Pay 5% of your Health Points)  #portrait:none #speaker:system
* #horse_l2_c1
    ~ MoveTo("MoneyBox")
    -> END
* #horse_l2_c2
    ~ MoveTo("EnemyBox")
    -> END
* #horse_l2_c3
    ~ MoveTo("ChurchBox")
    -> END
* #horse_l2_c4
    ~ MoveTo("BonfireBox")
    -> END
* #horse_l2_c5
    ~ MoveTo("ShopBox")
    -> END
* #horse_l2_c6
    ~ StartNextTurn()
    -> END

===NotEnoughSouls===
#souls_requirement_l1
You need 20 Souls to challenge the Protector of the Church.  #portrait:none #speaker:system
~StartNextTurn()
->END

===IchabodDialogue===
~StartBattle("Ichabod")
->END