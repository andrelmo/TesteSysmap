namespace WebVenda.Dal.Implementation
{
    public static class GeradorIdVenda
    {
        private static int _novoId;

        public static int GerarNovoId()
        {
            return (++_novoId);
        }

        static GeradorIdVenda()
        {
            _novoId = 0;
        }
    }
}