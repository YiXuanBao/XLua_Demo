Main = {}

function Main:Awake()
    print('Awake',self.MonoProxy.gameObject.name)
end

function Main:Start()
    print('Start', self.MonoProxy.gameObject.name)
    CS.UnityEngine.GameObject("1")
    CS.YXCell.YXUtils.EditorLogNormal("111")
end

function Main:OnDestroy()
    
end

return Main