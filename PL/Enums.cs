﻿

using System;
using System.Collections;
using System.Collections.Generic;

namespace PL;



public class EngineerExperienceCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> e_enums =
(Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;


    //    {
    //    static readonly IEnumerable e_enums = Enum.GetValues(typeof(BO.EngineerExperience));

    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
}


