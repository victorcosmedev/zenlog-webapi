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

        #region LogEmocionalController

        public const string LogEmocionalControllerSummary = "Gerencia as operações relacionadas aos logs emocionais.";

        public const string AdicionarLogAsyncSummary = "Adiciona um novo log emocional.";
        public const string AdicionarLogAsyncDescription = "Cria um novo log emocional no sistema.";

        public const string EditarLogAsyncSummary = "Edita um log emocional existente.";
        public const string EditarLogAsyncDescription = "Atualiza as informações de um log emocional existente com base no ID fornecido.";

        public const string RemoverLogAsyncSummary = "Remove um log emocional.";
        public const string RemoverLogAsyncDescription = "Remove um log emocional do sistema com base no ID fornecido.";

        public const string BuscarPorIdLogAsyncSummary = "Busca um log emocional por ID.";
        public const string BuscarPorIdLogAsyncDescription = "Retorna os detalhes de um log emocional com base no ID fornecido.";

        public const string ListarPorColaboradorAsyncSummary = "Lista logs emocionais por colaborador.";
        public const string ListarPorColaboradorAsyncDescription = "Retorna uma lista paginada de logs emocionais associados a um colaborador específico.";
        #endregion

        #region EmpresaController

        public const string EmpresaControllerSummary = "Gerencia as operações relacionadas às empresas.";

        public const string AdicionarEmpresaAsyncSummary = "Adiciona uma nova empresa.";
        public const string AdicionarEmpresaAsyncDescription = "Cria uma nova empresa no sistema.";

        public const string EditarEmpresaAsyncSummary = "Edita uma empresa existente.";
        public const string EditarEmpresaAsyncDescription = "Atualiza as informações de uma empresa existente com base no ID fornecido.";

        public const string RemoverEmpresaAsyncSummary = "Remove uma empresa.";
        public const string RemoverEmpresaAsyncDescription = "Remove uma empresa do sistema com base no ID fornecido.";

        public const string ListarEmpresasAsyncSummary = "Lista todas as empresas.";
        public const string ListarEmpresasAsyncDescription = "Retorna uma lista paginada de todas as empresas.";

        public const string BuscarPorIdEmpresaAsyncSummary = "Busca uma empresa por ID.";
        public const string BuscarPorIdEmpresaAsyncDescription = "Retorna os detalhes de uma empresa com base no ID fornecido.";
        #endregion

        #region HealthCheckDoc
        public const string LiveCheckSummary = "Verifica se a API está online (Liveness)";
        public const string LiveCheckDescription = "Verifica a saúde básica da aplicação (se está 'viva'). Retorna Healthy se a aplicação iniciou corretamente.";

        public const string ReadyCheckSummary = "Verifica se a API está pronta para receber tráfego (Readiness)";
        public const string ReadyCheckDescription = "Verifica a saúde das dependências externas (como o banco de dados Oracle). Retorna Healthy se a API e suas dependências estão prontas.";
        #endregion
    }
}
