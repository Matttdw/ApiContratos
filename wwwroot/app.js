// URL base da API de contratos
const API_URL = "/api/contratos";

// Cache em memória dos contratos carregados do backend
let contratosCache = [];

// carregar contratos do backend
async function carregarContratos() {
    const tbody = document.getElementById("tabelaContratos");
    tbody.innerHTML = "";

    try {
        const resposta = await fetch(API_URL);
        if (!resposta.ok) {
            throw new Error("Erro ao buscar contratos");
        }

        const dados = await resposta.json();
        contratosCache = dados;
        renderTabela(dados);
    } catch (e) {
        console.error(e);
        tbody.innerHTML = `
            <tr>
                <td colspan="10">Erro ao carregar contratos.</td>
            </tr>
        `;
    }
}

// renderiza a tabela de contratos
function renderTabela(lista) {
    const tbody = document.getElementById("tabelaContratos");
    tbody.innerHTML = "";

    if (!lista || lista.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="10">Nenhum contrato encontrado.</td>
            </tr>
        `;
        return;
    }

    lista.forEach((contrato) => {
        const linha = document.createElement("tr");

        linha.innerHTML = `
            <td>
                <button onclick="deletarContrato(${contrato.id})">Excluir</button>
            </td>
            <td>${contrato.id}</td>
            <td>${contrato.numero}</td>
            <td>${contrato.cliente}</td>
            <td>${contrato.dataInicio}</td>
            <td>${contrato.dataVencimento}</td>
            <td>${contrato.renovacaoAutomatica ? "Sim" : "Não"}</td>
            <td>${contrato.ativo ? "Ativo" : "Vencido"}</td>
            <td>${contrato.vencimentoEfetivo}</td>
            <td>${contrato.descricao}</td>
        `;

        tbody.appendChild(linha);
    });
}

// executa ao carregar a página
window.onload = carregarContratos;


// salvar novo contrato
async function salvarContrato() {

    const novoContrato = {
        numero: document.getElementById("cadNumero").value,
        cliente: document.getElementById("cadCliente").value,
        dataInicio: document.getElementById("cadDataInicio").value,
        dataVencimento: document.getElementById("cadDataVencimento").value,
        renovacaoAutomatica: document.getElementById("cadRenovacao").checked,
        descricao: document.getElementById("cadDescricao").value
    };

    const resposta = await fetch(API_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(novoContrato)
    });

    if (resposta.ok) {
        alert("Contrato cadastrado com sucesso!");
        limparCadastro();
        carregarContratos();
    } else {
        alert("Erro ao salvar contrato.");
    }
}

document.getElementById("btnSalvar").onclick = salvarContrato;


// deletar contrato
async function deletarContrato(id) {
    if (!confirm(`Tem certeza que deseja excluir o contrato ID ${id}?`)) {
        return;
    }

    const resposta = await fetch(`${API_URL}/${id}`, {
        method: "DELETE"
    });

    if (resposta.ok) {
        alert("Contrato removido!");
        carregarContratos();
    } else {
        alert("Erro ao deletar contrato.");
    }
}


// buscar contratos
function buscarContratos() {
    const id = document.getElementById("buscaId").value.trim();
    const numero = document.getElementById("buscaNumero").value.trim().toLowerCase();
    const cliente = document.getElementById("buscaCliente").value.trim().toLowerCase();

    let lista = contratosCache;

    if (id) {
        lista = lista.filter(c => String(c.id) === id);
    }

    if (numero) {
        lista = lista.filter(c =>
            (c.numero || "").toLowerCase().includes(numero)
        );
    }

    if (cliente) {
        lista = lista.filter(c =>
            (c.cliente || "").toLowerCase().includes(cliente)
        );
    }

    renderTabela(lista);
}

// limpar campos de busca
function limparBusca() {
    document.getElementById("buscaId").value = "";
    document.getElementById("buscaNumero").value = "";
    document.getElementById("buscaCliente").value = "";
    renderTabela(contratosCache);
}

// liga os botões de busca aos handlers
document.getElementById("btnBuscar").onclick = buscarContratos;
document.getElementById("btnLimpar").onclick = limparBusca;
document.getElementById("btnAtualizar").onclick = carregarContratos;


// limpar campos de cadastro
function limparCadastro() {
    document.getElementById("cadNumero").value = "";
    document.getElementById("cadCliente").value = "";
    document.getElementById("cadDataInicio").value = "";
    document.getElementById("cadDataVencimento").value = "";
    document.getElementById("cadRenovacao").checked = false;
    document.getElementById("cadDescricao").value = "";
}
