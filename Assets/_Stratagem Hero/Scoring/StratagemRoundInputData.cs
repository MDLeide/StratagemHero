using System;

[Serializable]
class StratagemRoundInputData
{
    public int TotalInputs => GoodInputs + BadInputs;
    public bool Perfect => BadInputs <= 0;
    public int GoodInputs;
    public int BadInputs;
    public int TotalStratagems;
}