public class BuildManager
{
    private Turret _selectedTurret;
    
    public Turret GetSelectedTurret()
    {
        return _selectedTurret;
    }
    
    public void SetSelectedTurret(Turret turret)
    {
        _selectedTurret = turret;
    }
}