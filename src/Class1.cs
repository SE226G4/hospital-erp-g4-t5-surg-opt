namespace HospitalERP_SurgOPT.src{
public class SurgeryManager{
public bool ValidateSurgeryScheduling(bool isPatientValid, bool isTeamAvailable, bool isToolsReady, bool isBedAvailable, bool isSterilized)
{
    if (isPatientValid) 
    {
        if (isTeamAvailable) 
        {
            if (isToolsReady) 
            {
                if (isBedAvailable) 
                {
                    if (isSterilized) 
                    {
                        return true; 
                    }
                }
            }
        }
    }
    return false;
}
}
}