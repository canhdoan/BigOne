﻿using UnityEngine;
using System.Collections.Generic;
using AI.BT;
public class BTDMStringConverter
{
    public BehaviourTreeDM m_Tree;
    public string m_Code;

    //TaskTool tTool = new TaskTool();
    char[] delimiterChars = { 'S', 'Q', 'T' };

    public string TaskToCode(Task task, string prefixCode)
    {
        string taskCode = "";
        if (task.m_Type == TaskType.SELECTOR)
        {
            taskCode = "S";
        }
        else if (task.m_Type == TaskType.SEQUENCER)
        {
            taskCode = "Q";
        }
        else
        {
            taskCode = "T";
            taskCode += (int)task.m_Type;
        }

        string code = prefixCode + taskCode + "\n";
        return code;
    }

    public void ConvertToCode(Task task, string prefixCode)
    {
        string taskToCode = TaskToCode(task, prefixCode);

        m_Code = m_Code + taskToCode;
        if (task.m_Type == TaskType.SELECTOR || task.m_Type == TaskType.SEQUENCER)
        {
            var cmptask = (Composite)task;
            int i = 0;
            foreach (var tsk in cmptask.children)
            {
                string childPrefix = taskToCode.Substring(0, taskToCode.Length - 3) + i.ToString();
                ConvertToCode(tsk, childPrefix);
                i++;
            }
        }
    }

    public string WriteTree()
    {
        m_Code = "";
        ConvertToCode(m_Tree.rootTask, "0");
        Debug.Log(m_Code);
        return m_Code;
    }

    public Task CodeToTask(string code)
    {
        string lastLetter = code.Substring(code.Length - 1);
        if (lastLetter == "Q")
        {
            Sequence sequenceTask = new Sequence();
            sequenceTask.m_BehaviourTree = m_Tree;
            sequenceTask.m_Type = TaskType.SEQUENCER;
            return sequenceTask;
        }
        else if (lastLetter == "S")
        {
            Selector selectorTask = new Selector();
            selectorTask.m_BehaviourTree = m_Tree;
            selectorTask.m_Type = TaskType.SELECTOR;
            return selectorTask;
        }
        else
        {
            char[] delimiterChars = { 'T' };
            string[] codeSplit = code.Split(delimiterChars);
            int taskType;
            System.Int32.TryParse(codeSplit[codeSplit.Length], out taskType);
            Task task = GetTask((TaskType)taskType);
            task.m_BehaviourTree = m_Tree;
            return task;
        }
    }

    public void BuildTreeFromCode()
    {
        char[] newlineChar = { '\n' };
        string[] codes = m_Code.Split(newlineChar);
        List<string> codesList = new List<string>(codes);

        Task rootTaskBase = CodeToTask(codes[0]);
        
        if (rootTaskBase.m_Type == TaskType.SELECTOR)
        {
            var rootTask = (Selector)rootTaskBase;
            rootTask.children = new List<Task>();
            m_Tree.rootTask = rootTask;
        }
        else
        {
            var rootTask = (Sequence)rootTaskBase;
            rootTask.children = new List<Task>();
            m_Tree.rootTask = rootTask;
        }
        //codesList.Remove(codes[0]);
        BuildTreeFromCode(m_Tree.rootTask, codes[0], codesList);

    }

    // Make it with the 3 possible kind of task polymorphism
    public void BuildTreeFromCode(Task task, string code, List<string> remainingCodes)
    {
        if (task.m_Type == TaskType.SELECTOR || task.m_Type == TaskType.SEQUENCER)
        {
            var cmpTask = task as Composite;
            cmpTask.children = new List<Task>();

            string parentPrefix = code.Split(delimiterChars)[0];
            int parentNestedLevel = parentPrefix.Length;

            foreach (var cd in remainingCodes)
            {
                string prefix = cd.Split(delimiterChars)[0];
                int nestedLevel = prefix.Length;
                if (nestedLevel - parentNestedLevel == 1)
                {
                    if (parentPrefix == prefix.Substring(0, prefix.Length-2))
                    {
                        Task thisTask = CodeToTask(cd);
                        cmpTask.children.Add(thisTask);
                        //remainingCodes.Remove(cd);
                        BuildTreeFromCode(thisTask, cd, remainingCodes);
                    }
                }
            }

        }
        
    }

    public Task GetTask(TaskType taskType)
    {
        Task thisTask;
        thisTask = InstantiateTask(taskType);
        thisTask.m_Type = taskType;
        return thisTask;
    }

    public Task InstantiateTask(TaskType taskType)
    {
        switch (taskType)
        {
            case TaskType.GUARD_ALARMED:
                return new ActionAlarmed();
            case TaskType.GUARD_CHAR_CHASE:
                return new ActionChase();
            case TaskType.GUARD_CHECK_NAVPOINT:
                return new ActionCheckNavPoint();
            case TaskType.GUARD_STOP:
                return new ActionHoldPosition();
            case TaskType.GUARD_PATROL:
                return new ActionPatrol();
            case TaskType.GUARD_RANDOM_PATROL:
                return new ActionRandomPatrol();
            case TaskType.GUARD_REACH_LAST_PERCIEVED_POSITION:
                return new ActionReachLastPosition();
            case TaskType.GUARD_WAIT_NAVPOINT:
                return new ActionWaitNavPoint();
            case TaskType.GUARD_IS_ALARMED:
                return new ConditionAlarmed();
            case TaskType.GUARD_IS_CURIOUS:
                return new ConditionCurious();
            case TaskType.GUARD_IS_WAITING:
                return new ConditionIsWaiting();
            case TaskType.GUARD_IS_LAST_PERCIEVED_POSITION_REACHED:
                return new ConditionLastHeardSeenPosition();
            case TaskType.GUARD_IS_LAST_NODE_REACHED:
                return new ConditionLastNodeReached();
            case TaskType.GUARD_IS_CHAR_VISIBLE:
                return new ConditionPlayerVisible();
            default:
                return null;
        }
    }
}
