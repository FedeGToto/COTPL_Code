using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization;

[SerializeField]
public class KatrinaAI : EnemyAI
{
    [Header("Book Toss")]
    [SerializeField] private float bookTossWeight;
    [SerializeField] private LocalizedString bookTossName;
    [SerializeField] private LocalizedString bookTossDesc;
    [SerializeField] private LocalizedString bookTossText;
    [SerializeField] private float attackMultiplier = 0.8f;

    [Header("Reading")]
    [SerializeField] private float readingWeight;
    [SerializeReference] private Spell readingSpell;

    [Header("Ancient Spell")]
    [SerializeField] private float spellWeight;
    [SerializeReference] private Spell ancientSpell;

    [Header("Mana Resurgence")]
    [SerializeField] private float manaWeight;
    [SerializeReference] private Spell manaResurgenceSpell;

    [Header("Hastur's Blessing")]
    [SerializeField] private float blessingWeight;
    [SerializeReference] private Spell blessingSpell;

    [Header("Hastur's Challenge")]
    [SerializeField] private Zone hasturZone;
    [SerializeField] private LocalizedString hasturZoneName;
    [SerializeField] private LocalizedString hasturZoneDesc;
    [SerializeField] private float challengeActivationHP;

    public override void EvaluateNextAction()
    {
        HasturChallenge();
        WeightedRandomSelector<string> options = new();

        options.AddOption(nameof(BookToss), bookTossWeight);

        // Should always have mana to cast Mana Resurgence - EDIT: now mana resurgence has not mana cost
        if (readingSpell.CostCheck(Owner.Unit))
        {
            options.AddOption(nameof(Reading), readingWeight);
        }
        //if (ancientSpell.CostCheck(Owner.Unit))
        //{
        //    options.AddOption(nameof(AncientSpell), spellWeight);
        //}
        if (manaResurgenceSpell.CostCheck(Owner.Unit))
        {
            options.AddOption(nameof(ManaResurgence), manaWeight);
        }
        if (blessingSpell.CostCheck(Owner.Unit))
        {
            options.AddOption(nameof(HasturBlessing), blessingWeight);
        }

        string selection = options.SelectRandom();

        switch (selection)
        {
            case nameof(BookToss):
                BookToss();
                break;
            case nameof(Reading):
                Reading();
                break;
            case nameof(AncientSpell):
                AncientSpell();
                break;
            case nameof(ManaResurgence):
                ManaResurgence();
                break;
            case nameof(HasturBlessing):
                HasturBlessing();
                break;
            default:
                throw new System.ArgumentOutOfRangeException("Function not found");
        }
    }

    public override string GetAttacksDescription()
    {
        return $"<b>{bookTossName.GetLocalizedString()}</b> - {bookTossDesc.GetLocalizedString()}\n\n" +
            $"<b>{readingSpell.SpellName.GetLocalizedString()}</b> - {readingSpell.SpellDescription.GetLocalizedString()}\n\n" +
            $"<b>{manaResurgenceSpell.SpellName.GetLocalizedString()}</b> - {manaResurgenceSpell.SpellDescription.GetLocalizedString()}\n\n" +
            /*$"<b>{ancientSpell.SpellName.GetLocalizedString()}</b> - {ancientSpell.SpellDescription.GetLocalizedString()}\n\n" +*/
            $"<b>{blessingSpell.SpellName.GetLocalizedString()}</b> - {blessingSpell.SpellDescription.GetLocalizedString()}\n\n" +
            $"<b>{hasturZoneName.GetLocalizedString()}</b> - {hasturZoneDesc.GetLocalizedString()}";
    }

    private void BookToss()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        battleSystem.UI.TextBox.SetText(bookTossText.GetLocalizedString(), () =>
        {
            Owner.Unit.PhysAttack(battleSystem.PlayerUnit, attackMultiplier: attackMultiplier);
            var enemyTurn = battleSystem.FSM.currentState as EnemyTurnState;
            enemyTurn.EndTurn(1.5f);
        });
    }

    private void Reading()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        readingSpell.Use(battleSystem.EnemyUnit);
    }

    private void ManaResurgence()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        manaResurgenceSpell.Use(battleSystem.EnemyUnit);
    }

    private void AncientSpell()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        ancientSpell.Use(battleSystem.EnemyUnit);
    }

    private void HasturBlessing()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        blessingSpell.Use(battleSystem.EnemyUnit);
    }

    private void HasturChallenge()
    {
        float normalHP = Owner.Unit.Character.HealthPoints.Value / Owner.Unit.Character.HealthPoints.MaxValue;

        if (normalHP > challengeActivationHP)
            return;

        Zone loadedZone = BattleSystem.Instance.CurrentZone;

        if (loadedZone != null && loadedZone.ZoneID == hasturZone.ZoneID)
            return;

        BattleSystem.Instance.AddZone(hasturZone);
    }
}
