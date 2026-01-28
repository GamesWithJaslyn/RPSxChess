public class Teleport_Rule : ITileRule
{
    public bool CanEnter(ITileModel tile, IPieceModel piece)
    {
        return false;
    }
}