# Simple Platformer

2.º Trabalho de Tecnologias de Desenvolvimento de VideoJogos.

Feito por João Antunes, a23478 e João Freitas, a23475.

## Simple Platformer

### Game1.cs

**Propriedades:**

1. **GraphicsDeviceManager graphics** : Gere as configurações gráficas do jogo, como a resolução.
2. **SpriteBatch spriteBatch** : Utilizado para desenhar texturas no ecrã.
3. **PlatformManager platformManager** : Gere as plataformas, inimigos, moedas e o jogador.
4. **InputManager inputManager** : Gere a entrada do utilizador, como o teclado.
5. **GameStateManager gameStateManager** : Gere o estado atual do jogo.
6. **GameState gameState** : Enum que indica o estado atual do jogo.
7. **Camera2D camera** : Representa a câmara 2D que segue o jogador.
8. **SpriteFont font** : Fonte utilizada para desenhar texto no ecrã.
9. **SoundEffect backgroundMusic, jumpSound, doorSound, winSound, coinSound** : Efeitos sonoros carregados.
10. **SoundEffectInstance backgroundMusicInstance, jumpSoundInstance, doorSoundInstance, winSoundInstance, coinSoundInstance** : Instâncias dos efeitos sonoros para controlar reprodução, volume, entre outros.

![img](https://i.imgur.com/dl7JdYW.png)

**Funções:**

**Game1()**

* Inicializa as propriedades gráficas, define o diretório de conteúdo e torna o cursor do rato visível. Define o estado inicial do jogo como `Menu.`![img](https://i.imgur.com/6jRhoxr.png)

**Initialize()**

* Cria instâncias do `PlatformManager`, `InputManager`, `GameStateManager` e `Camera2D`.
  ![img](https://i.imgur.com/Ubj7JZP.png)

**LoadContent()**

* Inicializa o `spriteBatch` e carrega a fonte para os menus.
* Carrega a música de fundo e os efeitos sonoros, configurando-os (por exemplo, volume e loop) e começa a reprodução da música de fundo.
* Atribui os efeitos sonoros ao `PlatformManager`.
  ![img](https://i.imgur.com/oZvJ4rp.png)

**ChangeState(GameState newState)**

* Altera o estado atual do jogo para o novo estado fornecido.

**Update(GameTime gameTime)**

* Verifica o estado atual do jogo e executa ações apropriadas:
  * **Menu** : Se a barra de espaço for pressionada, reinicia o jogo e muda para o estado `Game`.
  * **Game** : Atualiza o jogo chamando `gameStateManager.UpdateGame(gameTime)` e atualiza a posição da câmara para seguir o jogador. Se o jogador tocar na porta, toca o som da porta e avança para o próximo nível.
  * **Victory** : Se a barra de espaço for pressionada, retorna ao estado `Menu`.

![img](https://i.imgur.com/aojMaZk.png)

**Draw(GameTime gameTime)**

* Limpa o ecrã com uma cor de fundo.
* Inicia um `spriteBatch` com a transformação da câmara e desenha o conteúdo apropriado com base no estado do jogo:
  * **Menu** : Desenha o texto do menu.
  * **Game** : Desenha plataformas, jogador, inimigos, porta e moedas.
  * **Victory** : Toca o som de vitória e desenha o texto de vitória com o número de inimigos mortos e moedas coletadas.
* Termina o `spriteBatch` e inicia outro para desenhar o UI (neste caso, apenas o número de moedas coletadas).

![img](https://i.imgur.com/8fPl7U9.png)

### GameState.cs

O código define uma enumeração chamada `GameState` que representa os diferentes estados do jogo.

![img](https://i.imgur.com/3mYPWEv.png)

Este código é uma definição simples que facilita a gestão do fluxo do jogo, permitindo alternar entre diferentes estados de maneira organizada. Ao utilizar esta enumeração, o código do jogo pode verificar em que estado se encontra e executar a lógica apropriada para esse estado, melhorando a legibilidade e manutenção do código.

### System

#### Camera.cs

**Propriedades**

1. **Vector2 position** : Armazena a posição atual da câmara.
2. **float zoom** : Controla o nível de zoom da câmara.
3. **float rotation** : Armazena o ângulo de rotação da câmara em torno do eixo Z.
4. **Viewport viewport** : Representa a área de visualização atual da câmara.

![img](https://i.imgur.com/YRTohIh.png)

**Funções**

**Construtor da Câmara**

* **Camera2D(Viewport viewport)**
  * Inicializa a câmara com o viewport fornecido, define a posição inicial para (0,0), o zoom inicial para 1.0f (sem zoom) e a rotação inicial para 0.0f (sem rotação).

**Propriedade de Posição**

* **Vector2 Position**
  * **get** : Retorna a posição atual da câmara.
  * **set** : Define a nova posição da câmara.

**Propriedade de Zoom**

* **float Zoom**
  * **get** : Retorna o nível de zoom atual.
  * **set** : Define o novo nível de zoom.

Propriedade de Rotação

* **float Rotation**
  * **get** : Retorna o ângulo de rotação atual.
  * **set** : Define o novo ângulo de rotação.

![img](https://i.imgur.com/VqOBBmg.png)

**Método para Obter a Matriz de Visualização**

* **Matrix GetViewMatrix()**
  * Retorna a matriz de visualização da câmara, que é usada para transformar as coordenadas do mundo para as coordenadas da câmara. Esta matriz é criada aplicando uma série de transformações:
    * **Matrix.CreateTranslation(new Vector3(-position, 0.0f))** : Move a posição da câmara para a origem (inversa da posição atual).
    * **Matrix.CreateRotationZ(rotation)** : Aplica a rotação em torno do eixo Z.
    * **Matrix.CreateScale(zoom)** : Aplica o zoom.
    * **Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0.0f))** : Move a câmara para o centro do viewport.

**Método para Atualizar a Posição da Câmara**

* **void Update(Vector2 playerPosition)**
  * Atualiza a posição da câmara para que siga a posição do jogador. A câmara é centrada na posição do jogador, de modo a manter o jogador sempre visível no centro do ecrã.

![img](https://i.imgur.com/OewrWTr.png)

#### InputManager.cs

**Funções**

**Método para Obter a Direção do Movimento**

* **Vector2 GetMovementDirection()**
  * Este método determina a direção do movimento do jogador com base nas teclas pressionadas no teclado.
  * Inicializa um vetor `direction` com `Vector2.Zero`, o que significa que inicialmente não há movimento.
  * Obtém o estado atual do teclado utilizando `Keyboard.GetState()`.
  * Verifica se as teclas **A** ou **Left** estão pressionadas para mover para a esquerda, diminuindo o valor de `X` em 1.
  * Verifica se as teclas **D** ou **Right** estão pressionadas para mover para a direita, aumentando o valor de `X` em 1.
  * Retorna o vetor `direction` que indica a direção do movimento.

![img](https://i.imgur.com/TpKMUXG.png)

**Método para Verificar se o Jogador está a Tentar Saltar**

* **bool IsJumping()**
  * Este método verifica se o jogador está a tentar saltar com base nas teclas pressionadas no teclado.
  * Obtém o estado atual do teclado utilizando `Keyboard.GetState()`.
  * Verifica se as teclas  **W** , **Space** ou **Up** estão pressionadas. Se qualquer uma dessas teclas estiver pressionada, retorna `true`, indicando que o jogador está a tentar saltar.
  * Caso contrário, retorna `false`.

![img](https://i.imgur.com/zCRmV7a.png)

### Scripts

#### Managers

##### GameManager.cs

**Propriedades**

1. **PlatformManager platformManager** : Gere as plataformas, inimigos, moedas e o jogador.
2. **InputManager inputManager** : Gere a entrada do utilizador, como o teclado.
3. **Game1 game** : Referência para a instância do jogo principal, utilizada para mudar o estado do jogo.

**Funções**

**Construtor do GameStateManager**

* **GameStateManager(Game1 game, PlatformManager platformManager, InputManager inputManager)**
  * Inicializa o `GameStateManager` com referências ao jogo principal, `PlatformManager` e `InputManager`.

![img](https://i.imgur.com/H5cxl3M.png)

**Método para Atualizar o Estado do Jogo**

* **void UpdateGame(GameTime gameTime)**
  * Este método atualiza o estado do jogo com base nas entradas do utilizador e na lógica do jogo.
  * **Obtenção da Direção do Movimento e Verificação de Salto**
    * Obtém a direção do movimento do jogador utilizando `inputManager.GetMovementDirection()`.
    * Verifica se o jogador está a tentar saltar utilizando `inputManager.IsJumping()`.
  * **Atualização do Estado do Jogador**
    * Atualiza o estado do jogador chamando `platformManager.Player.Update(gameTime, direction, isJumping, platformManager.Platforms, platformManager.Coins, platformManager)`.
  * **Atualização dos Inimigos e Verificação de Colisões**
    * Cria uma lista `enemiesToRemove` para armazenar os inimigos que devem ser removidos.
    * Para cada inimigo em `platformManager.Enemies`, chama `enemy.Update(gameTime, platformManager.Player, platformManager.Platforms)` para atualizar o estado do inimigo.
    * Verifica se o retângulo do jogador colide com o retângulo de um inimigo utilizando `platformManager.Player.Rectangle.Intersects(enemy.Rectangle)`.
      * Se a parte inferior do jogador está acima da parte superior do inimigo e o jogador está a cair (`platformManager.Player.Velocity.Y > 0`), adiciona o inimigo à lista `enemiesToRemove` e incrementa o contador de inimigos mortos com `platformManager.addEnemyKilled()`.
      * Se não, reseta o nível chamando `platformManager.ResetLevel()` e quebra o ciclo.
  * **Remoção dos Inimigos Mortos**
    * Remove os inimigos em `enemiesToRemove` da lista de inimigos em `platformManager.Enemies`.
  * **Verificação da Tecla Esc para Retornar ao Menu**
    * Verifica se a tecla **Esc** foi pressionada utilizando `Keyboard.GetState().IsKeyDown(Keys.Escape)`. Se sim, muda o estado do jogo para `Menu` chamando `game.ChangeState(GameState.Menu)`.

![img](https://i.imgur.com/eTvIY5L.png)

##### PlatformManager.cs

**Propriedades**

![img](https://i.imgur.com/8XBbwyp.png)

1. **GraphicsDevice graphicsDevice** : Dispositivo gráfico utilizado para criar texturas e outros recursos gráficos.
2. **List `<Platform>` Platforms** : Lista que armazena todas as plataformas no jogo.
3. **Player Player** : Objeto que representa o jogador.
4. **List `<Enemy>` Enemies** : Lista que armazena todos os inimigos no jogo.
5. **Door Door** : Objeto que representa a porta que o jogador deve alcançar para completar o nível.
6. **List `<Coin>` Coins** : Lista que armazena todas as moedas no jogo.
7. **SoundEffectInstance jumpSound** : Instância do efeito sonoro do salto.
8. **SoundEffectInstance coinSound** : Instância do efeito sonoro da moeda.
9. **int CollectedCoins** : Contador de moedas coletadas.
10. **int EnemyKilled** : Contador de inimigos mortos.
11. **Texture2D platformTexture, playerTexture, enemyTexture, doorTexture, coinTexture** : Texturas para as plataformas, jogador, inimigos, porta e moedas.
12. **const int tileSize** : Tamanho dos tiles (32x32).
13. **string[][] levels** : Array de strings que representa os diferentes níveis do jogo.
14. **int currentLevelIndex** : Índice do nível atual.
15. **Game1 game** : Referência para a instância do jogo principal, utilizada para mudar o estado do jogo.

![img](https://i.imgur.com/lUx4S2F.png)

**Funções**

**Construtor do PlatformManager**

* **PlatformManager(GraphicsDevice graphicsDevice, Game1 game)**
  * Inicializa o `PlatformManager` com o dispositivo gráfico e o jogo principal.
  * Inicializa as listas de plataformas, inimigos e moedas, bem como os contadores de moedas coletadas e inimigos mortos.
  * Carrega as texturas e o nível atual.

![img](https://i.imgur.com/iKukRb3.png)

**Carregar Texturas**

* **LoadTextures()**
  * Carrega as texturas para plataformas, jogador, inimigos, porta e moedas a partir de ficheiros.

![img](https://i.imgur.com/quYti8P.png)

**Carregar um Nível Específico**

* **LoadLevel(int levelIndex)**
  * Limpa as listas de plataformas, inimigos e moedas.
  * Carrega o mapa do nível especificado e cria objetos de plataformas, jogador, inimigos, porta e moedas com base nos caracteres do mapa.

![img](https://i.imgur.com/EkWdwh2.png)

**Adicionar um Inimigo Morto ao Contador**

* **addEnemyKilled()**
  * Incrementa o contador de inimigos mortos.

**Adicionar uma Moeda Coletada ao Contador e Tocar o Som**

* **addCollectedCoins()**
  * Toca o som da moeda e incrementa o contador de moedas coletadas.

**Resetar o Nível Atual**

* **ResetLevel()**
  * Recarrega o nível atual.

![img](https://i.imgur.com/H28eVf7.png)

**Definir as Texturas para os Diferentes Elementos do Jogo**

* **SetTextures(Texture2D platformTexture, Texture2D playerTexture, Texture2D enemyTexture, Texture2D doorTexture, Texture2D coinTexture)**
  * Define as novas texturas para as plataformas, jogador, inimigos, porta e moedas.

![img](https://i.imgur.com/LzNBYyX.png)

**Carregar o Próximo Nível ou Mudar para o Estado de Vitória**

* **NextLevel()**
  * Se houver mais níveis, incrementa o índice do nível atual e carrega o próximo nível.
  * Caso contrário, muda o estado do jogo para `Victory`.

![img](https://i.imgur.com/bjHnqyS.png)

**Resetar o Jogo, Carregando o Primeiro Nível**

* **resetGame()**
  * Carrega o primeiro nível.

**Adicionar o Som do Salto**

* **addJumpSound(SoundEffectInstance jumpSound)**
  * Define o som do salto.

**Adicionar o Som da Moeda**

* **addCoinSound(SoundEffectInstance coinSound)**
  * Define o som da moeda.

![img](https://i.imgur.com/MZr8wxr.png)

**Desenhar as Plataformas no Ecrã**

* **DrawPlatforms(SpriteBatch spriteBatch)**
  * Desenha todas as plataformas.

**Desenhar o Jogador no Ecrã**

* **DrawPlayer(SpriteBatch spriteBatch)**
  * Desenha o jogador.

**Desenhar os Inimigos no Ecrã**

* **DrawEnemies(SpriteBatch spriteBatch)**
  * Desenha todos os inimigos.

**Desenhar as Moedas no Ecrã**

* **DrawCoins(SpriteBatch spriteBatch)**
  * Desenha todas as moedas.

**Desenhar a Porta no Ecrã**

* **DrawDoor(SpriteBatch spriteBatch)**
  * Desenha a porta, se existir.

![img](https://i.imgur.com/HwsaETt.png)

#### Objects

##### **Coin.cs**

**Propriedades**

1. **Texture2D Texture** : Esta propriedade armazena a textura da moeda. A textura define a aparência visual da moeda quando ela é desenhada no ecrã.

**Funções**

**Construtor da Classe Coin**

* **Coin(Rectangle rectangle, Texture2D texture)**
  * Este construtor inicializa uma nova instância da classe `Coin`.
  * Ele aceita um retângulo (`rectangle`) que define a posição e as dimensões da moeda e uma textura (`texture`) que define a aparência visual da moeda.
  * O construtor chama o construtor da classe base `CollisionBox` para inicializar a posição e o tamanho da moeda e depois atribui a textura à propriedade `Texture`.

![img](https://i.imgur.com/vg6ob9e.png)

**Método para Renderizar a Moeda no Ecrã**

* **void Draw(SpriteBatch spriteBatch)**
  * Este método desenha a moeda no ecrã utilizando um `SpriteBatch`.
  * Ele aceita um `SpriteBatch` como parâmetro, que é utilizado para desenhar texturas.
  * A moeda é desenhada na posição e com as dimensões definidas pelo retângulo `Rectangle`, usando a textura armazenada na propriedade `Texture`. A cor branca (`Color.White`) é utilizada para desenhar a textura sem modificações de cor.

**Método para Definir uma Nova Textura para a Moeda**

* **void SetTexture(Texture2D texture)**
  * Este método permite definir uma nova textura para a moeda.
  * Ele aceita uma textura (`texture`) como parâmetro e atribui essa textura à propriedade `Texture`.

##### CollisionBox.cs

**Propriedades**

1. **Rectangle Rectangle** : Esta propriedade armazena o retângulo de colisão que define a posição e as dimensões da caixa de colisão. Este retângulo é utilizado para detectar colisões com outros objetos no jogo.
2. **Vector2 Position** : Esta propriedade obtém e define a posição da caixa de colisão. Quando a posição é definida, o retângulo de colisão é atualizado com as novas coordenadas.

![img](https://i.imgur.com/RsKFKAz.png)

**Funções**

**Construtor da Classe CollisionBox**

* **CollisionBox(int x, int y, int width, int height)**
  * Este construtor inicializa uma nova instância da classe `CollisionBox`.
  * Ele aceita quatro parâmetros: `x` e `y` que definem as coordenadas do canto superior esquerdo da caixa de colisão, e `width` e `height` que definem a largura e altura da caixa de colisão.
  * O construtor cria um novo retângulo (`Rectangle`) com as coordenadas e dimensões fornecidas.

**Propriedade de Posição**

* **Vector2 Position**
  * **get** : Retorna a posição atual da caixa de colisão como um `Vector2`, utilizando as coordenadas `X` e `Y` do retângulo de colisão.
  * **set** : Define a nova posição da caixa de colisão. Quando a posição é definida, o retângulo de colisão (`Rectangle`) é atualizado com as novas coordenadas `X` e `Y`, mantendo as mesmas dimensões (`Width` e `Height`).

##### Door.cs

**Propriedades**

1. **Texture2D Texture** : Esta propriedade armazena a textura da porta. A textura define a aparência visual da porta quando ela é desenhada no ecrã.

**Funções**

**Construtor da Classe Door**

* **Door(Rectangle rectangle, Texture2D texture)**
  * Este construtor inicializa uma nova instância da classe `Door`.
  * Ele aceita um retângulo (`rectangle`) que define a posição e as dimensões da porta, e uma textura (`texture`) que define a aparência visual da porta.
  * O construtor chama o construtor da classe base `CollisionBox` para inicializar a posição e o tamanho da porta, e depois atribui a textura à propriedade `Texture`.

![img](https://i.imgur.com/QYG2YR4.png)

**Método para Desenhar a Porta no Ecrã**

* **void Draw(SpriteBatch spriteBatch)**
  * Este método desenha a porta no ecrã utilizando um `SpriteBatch`.
  * Ele aceita um `SpriteBatch` como parâmetro, que é utilizado para desenhar texturas.
  * A porta é desenhada na posição e com as dimensões definidas pelo retângulo `Rectangle`, usando a textura armazenada na propriedade `Texture`. A cor branca (`Color.White`) é utilizada para desenhar a textura sem modificações de cor.

**Método para Definir uma Nova Textura para a Porta**

* **void SetTexture(Texture2D texture)**
  * Este método permite definir uma nova textura para a porta.
  * Ele aceita uma textura (`texture`) como parâmetro e atribui essa textura à propriedade `Texture`.

##### Enemy.cs

**Propriedades**

1. **Texture2D Texture** : Esta propriedade armazena a textura do inimigo. A textura define a aparência visual do inimigo quando ele é desenhado no ecrã.
2. **Vector2 velocity** : Este campo armazena a velocidade atual do inimigo, utilizada para calcular seu movimento.
3. **float gravity** : Este campo define a gravidade aplicada ao inimigo, afetando sua velocidade vertical.
4. **float speed** : Este campo define a velocidade de movimento horizontal do inimigo.

![img](https://i.imgur.com/8AKu4fp.png)

**Funções**

**Construtor da Classe Enemy**

* **Enemy(Rectangle rectangle, Texture2D texture)**
  * Este construtor inicializa uma nova instância da classe `Enemy`.
  * Ele aceita um retângulo (`rectangle`) que define a posição e as dimensões do inimigo, e uma textura (`texture`) que define a aparência visual do inimigo.
  * O construtor chama o construtor da classe base `CollisionBox` para inicializar a posição e o tamanho do inimigo, e depois atribui a textura à propriedade `Texture`.
  * A velocidade inicial do inimigo (`velocity`) é definida como zero.

**Método para Atualizar o Estado do Inimigo**

* **void Update(GameTime gameTime, Player player, List `<Platform>` platforms)**
  * Este método atualiza o estado do inimigo a cada frame.
  * Define a direção do movimento do inimigo com base na posição do jogador: se o jogador está à direita, o inimigo move-se para a direita; se o jogador está à esquerda, o inimigo move-se para a esquerda.
  * Calcula a nova posição do inimigo adicionando a direção multiplicada pela velocidade à posição atual.
  * Aplica a gravidade à velocidade vertical do inimigo e atualiza a posição vertical.
  * Verifica colisões horizontais com as plataformas: se houver colisão, a posição horizontal não é atualizada.
  * Verifica colisões verticais com as plataformas: se houver colisão, a posição vertical não é atualizada e a velocidade vertical é zerada.
  * Atualiza a posição do inimigo com as novas coordenadas calculadas.

![img](https://i.imgur.com/e5NTSDs.png)

**Método para Verificar Colisões com as Plataformas**

* **bool CheckCollisions(Rectangle futureRect, List `<Platform>` platforms)**
  * Este método verifica se o retângulo futuro do inimigo colide com alguma plataforma.
  * Aceita um retângulo (`futureRect`) que representa a posição futura do inimigo, e uma lista de plataformas (`platforms`).
  * Itera por todas as plataformas e verifica se o retângulo futuro do inimigo intersecta o retângulo de alguma plataforma.
  * Retorna `true` se houver uma colisão, caso contrário, retorna `false`.

**Método para Renderizar o Inimigo na Tela**

* **void Draw(SpriteBatch spriteBatch)**
  * Este método desenha o inimigo no ecrã utilizando um `SpriteBatch`.
  * Aceita um `SpriteBatch` como parâmetro, que é utilizado para desenhar texturas.
  * Desenha o inimigo na posição e com as dimensões definidas pelo retângulo `Rectangle`, usando a textura armazenada na propriedade `Texture`. A cor branca (`Color.White`) é utilizada para desenhar a textura sem modificações de cor.

**Método para Definir uma Nova Textura para o Inimigo**

* **void SetTexture(Texture2D texture)**
  * Este método permite definir uma nova textura para o inimigo.
  * Aceita uma textura (`texture`) como parâmetro e atribui essa textura à propriedade `Texture`.

![img](https://i.imgur.com/YBXOHWc.png)

##### Platform.cs

**Propriedades**

1. **Texture2D Texture** : Esta propriedade armazena a textura da plataforma. A textura define a aparência visual da plataforma quando ela é desenhada no ecrã.

**Funções**

**Construtor da Classe Platform**

* **Platform(Rectangle rectangle, Texture2D texture)**
  * Este construtor inicializa uma nova instância da classe `Platform`.
  * Ele aceita um retângulo (`rectangle`) que define a posição e as dimensões da plataforma, e uma textura (`texture`) que define a aparência visual da plataforma.
  * O construtor chama o construtor da classe base `CollisionBox` para inicializar a posição e o tamanho da plataforma, e depois atribui a textura à propriedade `Texture`.

![img](https://i.imgur.com/HxbMc0W.png)

**Método para Desenhar a Plataforma no Ecrã**

* **void Draw(SpriteBatch spriteBatch)**
  * Este método desenha a plataforma no ecrã utilizando um `SpriteBatch`.
  * Ele aceita um `SpriteBatch` como parâmetro, que é utilizado para desenhar texturas.
  * A plataforma é desenhada na posição e com as dimensões definidas pelo retângulo `Rectangle`, usando a textura armazenada na propriedade `Texture`. A cor branca (`Color.White`) é utilizada para desenhar a textura sem modificações de cor.

**Método para Definir uma Nova Textura para a Plataforma**

* **void SetTexture(Texture2D texture)**
  * Este método permite definir uma nova textura para a plataforma.
  * Ele aceita uma textura (`texture`) como parâmetro e atribui essa textura à propriedade `Texture`.

##### Player.cs

**Propriedades**

1. **Vector2 Position** : Esta propriedade obtém e define a posição atual do jogador.
2. **Vector2 StartPosition** : Esta propriedade armazena a posição inicial do jogador, usada para resetar a posição do jogador.
3. **Texture2D Texture** : Esta propriedade armazena a textura do jogador. A textura define a aparência visual do jogador quando ele é desenhado no ecrã.
4. **bool IsOnGround** : Esta propriedade indica se o jogador está no chão.
5. **Vector2 velocity** : Este campo armazena a velocidade atual do jogador, utilizada para calcular seu movimento.
6. **Vector2 Velocity** : Propriedade que retorna a velocidade atual do jogador.
7. **float gravity** : Este campo define a gravidade aplicada ao jogador, afetando sua velocidade vertical.
8. **float jumpSpeed** : Este campo define a velocidade de salto do jogador.
9. **float jumpCooldown** : Este campo define o tempo de cooldown entre saltos em segundos.
10. **float timeSinceLastJump** : Este campo armazena o tempo desde o último salto.
11. **SoundEffectInstance jumpSound** : Esta propriedade armazena o som do salto.

![img](https://i.imgur.com/ronTxwA.png)

**Funções**

**Construtor da Classe Player**

* **Player(Rectangle rectangle, Texture2D texture, SoundEffectInstance jumpSound)**
  * Este construtor inicializa uma nova instância da classe `Player`.
  * Ele aceita um retângulo (`rectangle`) que define a posição e as dimensões do jogador, uma textura (`texture`) que define a aparência visual do jogador e um som de salto (`jumpSound`).
  * Inicializa a posição do jogador (`Position` e `StartPosition`), a textura (`Texture`), a flag de estar no chão (`IsOnGround`), e o som de salto (`jumpSound`).

![img](https://i.imgur.com/AkSrQjd.png)

**Método para Atualizar o Estado do Jogador**

* **void Update(GameTime gameTime, Vector2 direction, bool isJumping, List `<Platform>` platforms, List `<Coin>` coins, PlatformManager platformManager)**
  * Este método atualiza o estado do jogador a cada frame.
  * Atualiza o tempo de cooldown do salto.
  * Calcula a nova posição do jogador com base na direção do movimento.
  * Se o jogador está no chão e está a tentar saltar, aplica a velocidade de salto.
  * Aplica a gravidade à velocidade vertical do jogador e atualiza a posição vertical.
  * Verifica colisões horizontais e verticais com as plataformas.
  * Atualiza a posição do jogador com as novas coordenadas calculadas.
  * Verifica a coleta de moedas e atualiza o contador de moedas no `PlatformManager`.

![img](https://i.imgur.com/bObWbvJ.png)

**Método para Verificar Colisões com as Plataformas**

* **bool CheckCollisions(Rectangle futureRect, List `<Platform>`platforms)**
  * Este método verifica se o retângulo futuro do jogador colide com alguma plataforma.
  * Aceita um retângulo (`futureRect`) que representa a posição futura do jogador e uma lista de plataformas (`platforms`).
  * Itera por todas as plataformas e verifica se o retângulo futuro do jogador intersecta o retângulo de alguma plataforma.
  * Retorna `true` se houver uma colisão, caso contrário, retorna `false`.

![img](https://i.imgur.com/cW7m7Op.png)

**Método para Desenhar o Jogador na Tela**

* **void Draw(SpriteBatch spriteBatch)**
  * Este método desenha o jogador no ecrã utilizando um `SpriteBatch`.
  * Aceita um `SpriteBatch` como parâmetro, que é utilizado para desenhar texturas.
  * Desenha o jogador na posição e com as dimensões definidas pelo retângulo `Rectangle`, usando a textura armazenada na propriedade `Texture`. A cor branca (`Color.White`) é utilizada para desenhar a textura sem modificações de cor.

**Método para Definir uma Nova Textura para o Jogador**

* **void SetTexture(Texture2D texture)**
  * Este método permite definir uma nova textura para o jogador.
  * Aceita uma textura (`texture`) como parâmetro e atribui essa textura à propriedade `Texture`.

**Método para Resetar a Posição do Jogador**

* **void ResetPosition()**
  * Este método reseta a posição do jogador para a posição inicial.
  * A posição do jogador é definida como `StartPosition` e a velocidade é zerada (`velocity = Vector2.Zero`).

![img](https://i.imgur.com/xJkc52w.png)
