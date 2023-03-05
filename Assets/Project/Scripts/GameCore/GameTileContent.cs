using UnityEngine;

namespace Assets.Project.Scripts.GameCore
{
    public class GameTileContent : MonoBehaviour
    {
        [SerializeField] private GameTileContentType _type;

        public GameTileContentType Type => _type;

        public GameTileContentFactory OriginFactory { get; set; }

        public void Recycle()
        {
            OriginFactory.Reclain(this);
        }
    }

 

}