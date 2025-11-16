// URL base da API de contratos
const API_URL = "/api/contratos";

// Cache em memória dos contratos carregados
let contratosCache = [];

// --------- UTILITÁRIOS ---------

function formatarData(valor) {
    if (!valor) return "";
    const partes = valor.split("T")[0].split("-");
    if (partes.length !== 3) return valor;
    const [ano, mes, dia] = partes;
    return `${dia.padStart(2, "0")}/${mes.padStart(2, "0")}/${ano}`;
}

function calcularClasseStatus(contrato) {
    // Usa DataVencimento e RenovacaoAutomatica, igual à lógica do modelo
    if (!contrato.dataVencimento) return "";

    const hoje = new Date();
    hoje.setHours(0, 0, 0, 0);

    const [ano, mes, dia] = contrato.dataVencimento
        .split("T")[0]
        .split("-")
        .map(Number);

    if (!ano || !mes || !dia) return "";

    let venc = new Date(ano, mes - 1, dia);

    if (contrato.renovacaoAutomatica) {
        // se auto-renova, joga o vencimento pra frente até ficar >= hoje
        while (venc < hoje) {
            venc.setFullYear(venc.getFullYear() + 1);
        }
    }

    const diffDias = Math.round((venc - hoje) / (1000 * 60 * 60 * 24));

    if (diffDias < 0) return "table-danger";   // já passou
    if (diffDias <= 7) return "table-warning"; // prestes a vencer
    return "table-success";                    // ok
}

function badgeSimNao(valor) {
    if (valor === true) {
        return '<span class="badge-status badge-sim">Sim</span>';
    }
    return '<span class="badge-status badge-nao">Não</span>';
}

// --------- RENDERIZAÇÃO ---------

function renderTabela(lista) {
    const tbody = document.getElementById("tabelaContratos");
    tbody.innerHTML = "";

    if (!lista || lista.length === 0) {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td colspan="9" class="tabela-vazia">
                Nenhum contrato encontrado.
            </td>
        `;
        tbody.appendChild(row);
        return;
    }

    lista.forEach((contrato) => {
        const tr = document.createElement("tr");

        const classeStatus = calcularClasseStatus(contrato);
        if (classeStatus) tr.classList.add(classeStatus);

        tr.innerHTML = `
            <td>
                <div class="action-buttons">
                    <button class="btn btn-icon btn-edit" data-id="${contrato.id}" title="Editar">
                        <i class="bi bi-pencil"></i>
                    </button>
                    <button class="btn btn-icon btn-delete" data-id="${contrato.id}" title="Excluir">
                        <i class="bi bi-trash"></i>
                    </button>
                    <button class="btn btn-icon btn-view" data-id="${contrato.id}" title="Visualizar">
                        <i class="bi bi-eye"></i>
                    </button>
                </div>
            </td>
            <td>${contrato.id}</td>
            <td>${contrato.numero}</td>
            <td>${contrato.cliente}</td>
            <td>${formatarData(contrato.dataInicio)}</td>
            <td>${formatarData(contrato.dataVencimento)}</td>
            <td>${badgeSimNao(contrato.renovacaoAutomatica)}</td>
            <td>${badgeSimNao(contrato.ativo)}</td>
            <td>${contrato.descricao ?? ""}</td>
        `;

        tbody.appendChild(tr);
    });
}

// --------- API ---------

async function carregarContratos() {
    try {
        const resp = await fetch(API_URL);
        if (!resp.ok) throw new Error("Erro ao carregar contratos");
        const dados = await resp.json();
        contratosCache = Array.isArray(dados) ? dados : [];
        renderTabela(contratosCache);
    } catch (err) {
        console.error(err);
        alert("Erro ao carregar contratos. Verifique se a API está rodando.");
    }
}

async function salvarContrato(event) {
    event.preventDefault();

    const numero = document.getElementById("cadNumero").value.trim();
    const cliente = document.getElementById("cadCliente").value.trim();
    const dataInicio = document.getElementById("cadDataInicio").value;
    const dataVencimento = document.getElementById("cadDataVencimento").value;
    const renovacaoAutomatica =
        document.getElementById("cadRenovacao").checked;
    const descricao = document.getElementById("cadDescricao").value.trim();

    if (!numero || !cliente) {
        alert("Preencha pelo menos Número e Cliente.");
        return;
    }

    const novoContrato = {
        numero,
        cliente,
        dataInicio: dataInicio || null,
        dataVencimento: dataVencimento || null,
        renovacaoAutomatica,
        descricao
    };

    try {
        const resp = await fetch(API_URL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(novoContrato)
        });

        if (!resp.ok) {
            const erro = await resp.json().catch(() => ({}));
            throw new Error(erro.message || "Erro ao salvar contrato.");
        }

        await carregarContratos(); // atualiza lista
        limparCadastro();
        alert("Contrato salvo com sucesso!");
    } catch (err) {
        console.error(err);
        alert(err.message || "Erro ao salvar contrato.");
    }
}

async function deletarContrato(id) {
    if (!confirm(`Tem certeza que deseja excluir o contrato ${id}?`)) return;

    try {
        const resp = await fetch(`${API_URL}/${id}`, { method: "DELETE" });
        if (!resp.ok) {
            const erro = await resp.json().catch(() => ({}));
            throw new Error(erro.message || "Erro ao excluir contrato.");
        }
        await carregarContratos();
        alert(`Contrato ${id} removido com sucesso.`);
    } catch (err) {
        console.error(err);
        alert(err.message || "Erro ao excluir contrato.");
    }
}

// --------- BUSCA ---------

function buscarPorId(event) {
    event.preventDefault();
    const idStr = document.getElementById("buscaId").value.trim();

    if (!idStr) {
        // sem filtro → mostra todos
        renderTabela(contratosCache);
        return;
    }

    const lista = contratosCache.filter(
        (c) => String(c.id) === String(idStr)
    );
    renderTabela(lista);
}

function limparBusca() {
    document.getElementById("buscaId").value = "";
    renderTabela(contratosCache);
}

// --------- FORM ---------

function limparCadastro() {
    document.getElementById("cadId").value = "";
    document.getElementById("cadNumero").value = "";
    document.getElementById("cadCliente").value = "";
    document.getElementById("cadDataInicio").value = "";
    document.getElementById("cadDataVencimento").value = "";
    document.getElementById("cadRenovacao").checked = false;
    document.getElementById("cadDescricao").value = "";
}

// Preenche form (view/edição local)
function preencherFormulario(contrato) {
    document.getElementById("cadId").value = contrato.id;
    document.getElementById("cadNumero").value = contrato.numero;
    document.getElementById("cadCliente").value = contrato.cliente;
    document.getElementById("cadDataInicio").value = contrato.dataInicio
        ? contrato.dataInicio.split("T")[0]
        : "";
    document.getElementById("cadDataVencimento").value =
        contrato.dataVencimento
            ? contrato.dataVencimento.split("T")[0]
            : "";
    document.getElementById("cadRenovacao").checked =
        !!contrato.renovacaoAutomatica;
    document.getElementById("cadDescricao").value = contrato.descricao ?? "";
}

// --------- EVENTOS ---------

document.addEventListener("DOMContentLoaded", () => {
    // carrega lista inicial
    carregarContratos();

    // cadastro
    document
        .getElementById("formCadastro")
        .addEventListener("submit", salvarContrato);
    document
        .getElementById("btnLimparCadastro")
        .addEventListener("click", limparCadastro);

    // busca
    document
        .getElementById("formBusca")
        .addEventListener("submit", buscarPorId);
    document
        .getElementById("btnLimparBusca")
        .addEventListener("click", limparBusca);

    // atualizar lista
    document
        .getElementById("btnAtualizar")
        .addEventListener("click", carregarContratos);

    // ações na tabela
    const tbody = document.getElementById("tabelaContratos");
    tbody.addEventListener("click", (e) => {
        const btn = e.target.closest("button");
        if (!btn) return;

        const id = btn.getAttribute("data-id");
        if (!id) return;

        if (btn.classList.contains("btn-delete")) {
            deletarContrato(id);
            return;
        }

        const contrato = contratosCache.find(
            (c) => String(c.id) === String(id)
        );
        if (!contrato) {
            alert("Contrato não encontrado na lista carregada.");
            return;
        }

        preencherFormulario(contrato);

        if (btn.classList.contains("btn-view")) {
            alert("Contrato carregado no formulário para visualização.");
        }
    });
});
