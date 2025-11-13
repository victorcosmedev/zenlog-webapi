namespace ZenLogAPI.Utils.Doc
{
    public static class ApiDoc
    {
        #region ColaboradorController
        public const string ColaboradorControllerSummary = "Gerencia as operações relacionadas aos colaboradores.";

        public const string AdicionarAsyncSummary = "Adiciona um novo colaborador.";
        public const string AdicionarAsyncDescription = "Cria um novo colaborador no sistema.";

        public const string EditarAsyncSummary = "Edita um colaborador existente.";
        public const string EditarAsyncDescription = "Atualiza as informações de um colaborador existente com base no ID fornecido.";

        public const string RemoverAsyncSummary = "Remove um colaborador.";
        public const string RemoverAsyncDescription = "Remove um colaborador do sistema com base no ID fornecido.";

        public const string ListarAsyncSummary = "Lista todos os colaboradores.";
        public const string ListarAsyncDescription = "Retorna uma lista paginada de todos os colaboradores.";

        public const string ListarPorEmpresaSummary = "Lista colaboradores por empresa.";
        public const string ListarPorEmpresaDescription = "Retorna uma lista paginada de colaboradores associados a uma empresa específica.";

        public const string BuscarPorIdAsyncSummary = "Busca um colaborador por ID.";
        public const string BuscarPorIdAsyncDescription = "Retorna os detalhes de um colaborador com base no ID fornecido.";

        public const string BuscarPorEmailAsyncSummary = "Busca um colaborador por email.";
        public const string BuscarPorEmailAsyncDescription = "Retorna os detalhes de um colaborador com base no email fornecido.";

        public const string BuscarPorCpfAsyncSummary = "Busca um colaborador por CPF.";
        public const string BuscarPorCpfAsyncDescription = "Retorna os detalhes de um colaborador com base no CPF fornecido.";

        public const string BuscarPorMatriculaAsyncSummary = "Busca um colaborador por número de matrícula.";
        public const string BuscarPorMatriculaAsyncDescription = "Retorna os detalhes de um colaborador com base no número de matrícula fornecido.";

        #endregion
    }
}
