CREATE DATABASE IF NOT EXISTS vendas_gestao1;
USE vendas_gestao1;

CREATE TABLE funcionarios (
    id_funcionario INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(55),
    cpf VARCHAR(16)
);

CREATE TABLE dispositivos(
    id_dispositivo INT PRIMARY KEY AUTO_INCREMENT,
    alicota VARCHAR(10),
    descricao LONGTEXT,
    status_dispo VARCHAR(200)
);

CREATE TABLE caixas(
    id_caixa INT PRIMARY KEY AUTO_INCREMENT,
    saldo_inicial DOUBLE,
    total_entradas DOUBLE,
    total_saidas DOUBLE,
    saldo_final DOUBLE,
    status_caixa VARCHAR(255),
    fk_funcionario INT NOT NULL,
    FOREIGN KEY (fk_funcionario) REFERENCES funcionarios(id_funcionario)
);

CREATE TABLE despesas(
    id_despesa INT PRIMARY KEY AUTO_INCREMENT,
    valor DOUBLE,
    data_vencimento DATE,
    data_pagamento DATE,
    status_despesa VARCHAR(255),
    fk_caixa INT,
    FOREIGN KEY (fk_caixa) REFERENCES caixas(id_caixa)
);

CREATE TABLE clientes(
    id_cliente INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(55),
    cpf VARCHAR(16),
    email VARCHAR(55),
    telefone VARCHAR(23),
    data_nascimento DATE
);

CREATE TABLE servicos (
    id_servico INT PRIMARY KEY AUTO_INCREMENT,
    valor DOUBLE,
    descricao LONGTEXT,
    tempo TIME
);

CREATE TABLE vendas (
    id_venda INT PRIMARY KEY AUTO_INCREMENT,
    data_venda DATE,
    hora TIME,
    valor_total DOUBLE,
    total_parcelas int,
    desconto DOUBLE,
    valor_final DOUBLE,
    tipo VARCHAR(200),
    fk_cliente INT,
    FOREIGN KEY (fk_cliente) REFERENCES clientes(id_cliente)
);

CREATE TABLE vendas_servico (
    id_venda_servico INT PRIMARY KEY AUTO_INCREMENT,
    fk_venda INT,
    fk_servico INT,
    valor_unitario DOUBLE,
    quantidade INT,
    FOREIGN KEY (fk_venda) REFERENCES vendas(id_venda) ON DELETE CASCADE,
    FOREIGN KEY (fk_servico) REFERENCES servicos(id_servico)
);

CREATE TABLE recebimentos (
    id_recebimento INT PRIMARY KEY AUTO_INCREMENT,
    valor DOUBLE,
    data_vencimento DATE,
    data_pagamento DATE,
    status_recebimento VARCHAR(55),
    fk_caixa INT,
    fk_venda INT,
    FOREIGN KEY (fk_venda) REFERENCES vendas(id_venda) ON DELETE CASCADE,
    FOREIGN KEY (fk_caixa) REFERENCES caixas(id_caixa)
);

CREATE TABLE encargos (
    id_encargo INT PRIMARY KEY AUTO_INCREMENT,
    valor DOUBLE,
    descricao LONGTEXT,
    fk_recebimento INT,
    FOREIGN KEY (fk_recebimento) REFERENCES recebimentos(id_recebimento)
);

insert into clientes (nome, cpf, email, telefone, data_nascimento) values ('Theo Theodoro Novais', '02920920909', 'theo.tn@gmail.com', '920940928', '2005-10-21');

insert into clientes (nome, cpf, email, telefone, data_nascimento) values ('Gabriel de Oliveira Silva', '2092423523', 'gabriel.dos@gmail.com', '920940928', '2004-09-26');


insert into funcionarios(nome,cpf) values ('Joaquin Buckley','02202464670');


insert into caixas (
    saldo_inicial,
    total_entradas,
    total_saidas,
    saldo_final ,
    status_caixa,
    fk_funcionario
    ) 
    VALUES (0,0,0,0,'Aberto',1);


insert into servicos (valor, descricao, tempo) values (150, "lavagem completa", "03:00:00"), (200, "polimento", "05:00:00");