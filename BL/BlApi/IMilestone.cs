﻿namespace BlApi;
public interface IMilestone
{
    public BO.Milestone? ReadMilestoneData(int id);
    public BO.Milestone UpdateMilestoneData(BO.Milestone item);
    public void CreateScheduledProject();//לוז הפרוייקט
}