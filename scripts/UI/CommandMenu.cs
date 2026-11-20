using Godot;

enum CombatAction {
    Attack,
    Ability,
    Defend,
    Nurture
}


public partial class CommandMenu : Control
{
    
    [Signal] public delegate void ActionChosenEventHandler(int action);

   
    private Button _attackButton;  
    private Button _abilityButton; 
    private Button _defendButton; 
    private Button _nurtureButton;

    private bool _locked;

    public override void _Ready()
    {
        
        _attackButton = GetNode<Button>("%AttackButton");
        _abilityButton = GetNode<Button>("%AbilityButton");   // Looks up the Ability button.
        _defendButton = GetNode<Button>("%DefendButton");     // Looks up the Defend button.
        _nurtureButton = GetNode<Button>("%NurtureButton");   // Looks up the Nurture button.
        
        _attackButton.Pressed += () => OnActionPressed(CombatAction.Attack);

        _abilityButton.Pressed += () => OnActionPressed(CombatAction.Ability);  // Greys out Ability.
        _defendButton.Disabled = true;   // Greys out Defend.
        _nurtureButton.Disabled = true;  // Greys out Nurture.
        
        Lock();
    }
    
    public void Open()
    {
        _locked = false;           
        Show();                    
        _attackButton.GrabFocus(); 
    }
    
    public void Lock()
    {
        _locked = true; 
        Hide();         
    }
    
    private void OnActionPressed(CombatAction action)
    {
        if (_locked) {
            return;
        }  

        Lock();
        
        EmitSignal(SignalName.ActionChosen, (int)action);
    }
}