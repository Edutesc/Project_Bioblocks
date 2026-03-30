using System;
using System.Threading.Tasks;

public interface IPlayerLevelService
{
    // Incrementa o contador de questões válidas respondidas
    Task IncrementTotalAnswered();

    // Verifica se houve level up após o incremento
    Task CheckAndHandleLevelUp();

    // Recalcula o total a partir do Firebase
    Task RecalculateTotalAnswered();

    // Notifica quando dados do usuário são carregados
    void OnUserDataLoaded(UserData userData);

    // Getters de estado atual
    int GetCurrentLevel();
    int GetTotalValidAnswered();
    int GetTotalQuestionsInAllDatabanks();
    float GetProgressInCurrentLevel();
    int GetQuestionsUntilNextLevel();

    // Eventos
    event Action<int, int> OnLevelChanged;       // oldLevel → newLevel
    event Action<int> OnLevelProgressUpdated;     // totalAnswered atual
}
