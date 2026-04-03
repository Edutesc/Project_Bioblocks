// using UnityEngine;
// using LiteDB;
// public class LiteDBMockTest : MonoBehaviour
// {
//     void Start()
// {
//  var db = LiteDBService.Instance;
//  var loaded = db.GetUser("mock-user-123");
//  if (loaded != null)
//  Debug.Log($"[PERSISTÊNCIA] Dado encontrado: {loaded.NickName}, Score:
// {loaded.Score}");
//  else
//  Debug.Log("[PERSISTÊNCIA] Nenhum dado encontrado — rode a cena anterior primeiro.");
// }

// }


// using UnityEngine;

// public class LiteDBMockTest : MonoBehaviour
// {
//     void Start()
//     {
//         var user = LiteDBService.Instance.GetUser("mock-user-123");
        
//         // Usa uma única string para o log, facilitando a leitura [1]
//         string status = (user != null) 
//             ? $"[PERSISTÊNCIA] {user.NickName} (Nível {user.PlayerLevel}) carregado com sucesso!" 
//             : "[AVISO] Usuário não encontrado. Rode a cena de teste da Etapa 4 primeiro.";
            
//         Debug.Log(status);
//     }
// }