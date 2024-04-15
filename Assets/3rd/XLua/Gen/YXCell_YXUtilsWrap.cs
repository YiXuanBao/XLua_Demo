#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class YXCellYXUtilsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(YXCell.YXUtils);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 20, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Quit", _m_Quit_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ConvertJsonString", _m_ConvertJsonString_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EditorLogNormal", _m_EditorLogNormal_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EditorLogWarning", _m_EditorLogWarning_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EditorLogError", _m_EditorLogError_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EditorLogCritical", _m_EditorLogCritical_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DebugDrawCircle", _m_DebugDrawCircle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DebugDrawRectangle", _m_DebugDrawRectangle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DebugDrawText", _m_DebugDrawText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DebugDrawChar", _m_DebugDrawChar_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DebugDrawLines", _m_DebugDrawLines_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "InvokeAction", _m_InvokeAction_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "InvokeActionUnlimited", _m_InvokeActionUnlimited_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CancelInvoke", _m_CancelInvoke_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StartProcedure", _m_StartProcedure_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StartProcedureUnscaled", _m_StartProcedureUnscaled_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StopProcedure", _m_StopProcedure_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StartCoroutine", _m_StartCoroutine_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StopCoroutine", _m_StopCoroutine_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new YXCell.YXUtils();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to YXCell.YXUtils constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Quit_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    YXCell.YXUtils.Quit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConvertJsonString_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = YXCell.YXUtils.ConvertJsonString( _str );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EditorLogNormal_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    object _message = translator.GetObject(L, 1, typeof(object));
                    bool _detailed = LuaAPI.lua_toboolean(L, 2);
                    
                    YXCell.YXUtils.EditorLogNormal( _message, _detailed );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _message = translator.GetObject(L, 1, typeof(object));
                    
                    YXCell.YXUtils.EditorLogNormal( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YXCell.YXUtils.EditorLogNormal!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EditorLogWarning_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    object _message = translator.GetObject(L, 1, typeof(object));
                    bool _detailed = LuaAPI.lua_toboolean(L, 2);
                    
                    YXCell.YXUtils.EditorLogWarning( _message, _detailed );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _message = translator.GetObject(L, 1, typeof(object));
                    
                    YXCell.YXUtils.EditorLogWarning( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YXCell.YXUtils.EditorLogWarning!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EditorLogError_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    object _message = translator.GetObject(L, 1, typeof(object));
                    bool _detailed = LuaAPI.lua_toboolean(L, 2);
                    
                    YXCell.YXUtils.EditorLogError( _message, _detailed );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _message = translator.GetObject(L, 1, typeof(object));
                    
                    YXCell.YXUtils.EditorLogError( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YXCell.YXUtils.EditorLogError!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EditorLogCritical_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    object _message = translator.GetObject(L, 1, typeof(object));
                    bool _detailed = LuaAPI.lua_toboolean(L, 2);
                    
                    YXCell.YXUtils.EditorLogCritical( _message, _detailed );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _message = translator.GetObject(L, 1, typeof(object));
                    
                    YXCell.YXUtils.EditorLogCritical( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YXCell.YXUtils.EditorLogCritical!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DebugDrawCircle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Vector3 _center;translator.Get(L, 1, out _center);
                    float _radius = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Color _color;translator.Get(L, 3, out _color);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    int _divisions = LuaAPI.xlua_tointeger(L, 5);
                    
                    YXCell.YXUtils.DebugDrawCircle( _center, _radius, _color, _duration, _divisions );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DebugDrawRectangle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Vector3 _minXY;translator.Get(L, 1, out _minXY);
                    UnityEngine.Vector3 _maxXY;translator.Get(L, 2, out _maxXY);
                    UnityEngine.Color _color;translator.Get(L, 3, out _color);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    YXCell.YXUtils.DebugDrawRectangle( _minXY, _maxXY, _color, _duration );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DebugDrawText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Color _color;translator.Get(L, 3, out _color);
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    YXCell.YXUtils.DebugDrawText( _text, _position, _color, _size, _duration );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DebugDrawChar_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    char _c = (char)LuaAPI.xlua_tointeger(L, 1);
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    UnityEngine.Color _color;translator.Get(L, 3, out _color);
                    float _size = (float)LuaAPI.lua_tonumber(L, 4);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 5);
                    
                    YXCell.YXUtils.DebugDrawChar( _c, _position, _color, _size, _duration );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DebugDrawLines_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3>(L, 1)&& translator.Assignable<UnityEngine.Color>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<UnityEngine.Vector3[]>(L, 5)) 
                {
                    UnityEngine.Vector3 _position;translator.Get(L, 1, out _position);
                    UnityEngine.Color _color;translator.Get(L, 2, out _color);
                    float _size = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    UnityEngine.Vector3[] _points = (UnityEngine.Vector3[])translator.GetObject(L, 5, typeof(UnityEngine.Vector3[]));
                    
                    YXCell.YXUtils.DebugDrawLines( _position, _color, _size, _duration, _points );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3>(L, 1)&& translator.Assignable<UnityEngine.Color>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<float[]>(L, 5)) 
                {
                    UnityEngine.Vector3 _position;translator.Get(L, 1, out _position);
                    UnityEngine.Color _color;translator.Get(L, 2, out _color);
                    float _size = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    float[] _points = (float[])translator.GetObject(L, 5, typeof(float[]));
                    
                    YXCell.YXUtils.DebugDrawLines( _position, _color, _size, _duration, _points );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YXCell.YXUtils.DebugDrawLines!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InvokeAction_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<System.Action>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& (LuaAPI.lua_isnil(L, 5) || LuaAPI.lua_type(L, 5) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action>(L, 6)) 
                {
                    float _seconds = (float)LuaAPI.lua_tonumber(L, 1);
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 2);
                    int _repeatCount = LuaAPI.xlua_tointeger(L, 3);
                    float _repeatInterval = (float)LuaAPI.lua_tonumber(L, 4);
                    string _id = LuaAPI.lua_tostring(L, 5);
                    System.Action _onFinish = translator.GetDelegate<System.Action>(L, 6);
                    
                    YXCell.YXUtils.InvokeAction( _seconds, _callback, _repeatCount, _repeatInterval, _id, _onFinish );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<System.Action>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& (LuaAPI.lua_isnil(L, 5) || LuaAPI.lua_type(L, 5) == LuaTypes.LUA_TSTRING)) 
                {
                    float _seconds = (float)LuaAPI.lua_tonumber(L, 1);
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 2);
                    int _repeatCount = LuaAPI.xlua_tointeger(L, 3);
                    float _repeatInterval = (float)LuaAPI.lua_tonumber(L, 4);
                    string _id = LuaAPI.lua_tostring(L, 5);
                    
                    YXCell.YXUtils.InvokeAction( _seconds, _callback, _repeatCount, _repeatInterval, _id );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<System.Action>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _seconds = (float)LuaAPI.lua_tonumber(L, 1);
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 2);
                    int _repeatCount = LuaAPI.xlua_tointeger(L, 3);
                    float _repeatInterval = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    YXCell.YXUtils.InvokeAction( _seconds, _callback, _repeatCount, _repeatInterval );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<System.Action>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _seconds = (float)LuaAPI.lua_tonumber(L, 1);
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 2);
                    int _repeatCount = LuaAPI.xlua_tointeger(L, 3);
                    
                    YXCell.YXUtils.InvokeAction( _seconds, _callback, _repeatCount );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<System.Action>(L, 2)) 
                {
                    float _seconds = (float)LuaAPI.lua_tonumber(L, 1);
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 2);
                    
                    YXCell.YXUtils.InvokeAction( _seconds, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YXCell.YXUtils.InvokeAction!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InvokeActionUnlimited_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<System.Action>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TSTRING)) 
                {
                    float _seconds = (float)LuaAPI.lua_tonumber(L, 1);
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 2);
                    float _repeatInterval = (float)LuaAPI.lua_tonumber(L, 3);
                    string _id = LuaAPI.lua_tostring(L, 4);
                    
                    YXCell.YXUtils.InvokeActionUnlimited( _seconds, _callback, _repeatInterval, _id );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<System.Action>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _seconds = (float)LuaAPI.lua_tonumber(L, 1);
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 2);
                    float _repeatInterval = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    YXCell.YXUtils.InvokeActionUnlimited( _seconds, _callback, _repeatInterval );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<System.Action>(L, 2)) 
                {
                    float _seconds = (float)LuaAPI.lua_tonumber(L, 1);
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 2);
                    
                    YXCell.YXUtils.InvokeActionUnlimited( _seconds, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YXCell.YXUtils.InvokeActionUnlimited!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CancelInvoke_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _id = LuaAPI.lua_tostring(L, 1);
                    
                    YXCell.YXUtils.CancelInvoke( _id );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartProcedure_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<YXCell.YXCurve>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Action<float>>(L, 3)&& translator.Assignable<System.Action<float>>(L, 4)&& (LuaAPI.lua_isnil(L, 5) || LuaAPI.lua_type(L, 5) == LuaTypes.LUA_TSTRING)) 
                {
                    YXCell.YXCurve _curve = (YXCell.YXCurve)translator.GetObject(L, 1, typeof(YXCell.YXCurve));
                    float _time = (float)LuaAPI.lua_tonumber(L, 2);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 3);
                    System.Action<float> _onFinish = translator.GetDelegate<System.Action<float>>(L, 4);
                    string _id = LuaAPI.lua_tostring(L, 5);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedure( _curve, _time, _action, _onFinish, _id );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<YXCell.YXCurve>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Action<float>>(L, 3)&& translator.Assignable<System.Action<float>>(L, 4)) 
                {
                    YXCell.YXCurve _curve = (YXCell.YXCurve)translator.GetObject(L, 1, typeof(YXCell.YXCurve));
                    float _time = (float)LuaAPI.lua_tonumber(L, 2);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 3);
                    System.Action<float> _onFinish = translator.GetDelegate<System.Action<float>>(L, 4);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedure( _curve, _time, _action, _onFinish );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<YXCell.YXCurve>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Action<float>>(L, 3)) 
                {
                    YXCell.YXCurve _curve = (YXCell.YXCurve)translator.GetObject(L, 1, typeof(YXCell.YXCurve));
                    float _time = (float)LuaAPI.lua_tonumber(L, 2);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 3);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedure( _curve, _time, _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 8&& translator.Assignable<YXCell.YXCurve>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action<float>>(L, 5)&& translator.Assignable<System.Action<float>>(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)&& (LuaAPI.lua_isnil(L, 8) || LuaAPI.lua_type(L, 8) == LuaTypes.LUA_TSTRING)) 
                {
                    YXCell.YXCurve _curve = (YXCell.YXCurve)translator.GetObject(L, 1, typeof(YXCell.YXCurve));
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _timeParam = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 5);
                    System.Action<float> _onFinish = translator.GetDelegate<System.Action<float>>(L, 6);
                    bool _allowMultipleInstances = LuaAPI.lua_toboolean(L, 7);
                    string _id = LuaAPI.lua_tostring(L, 8);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedure( _curve, _startValue, _endValue, _timeParam, _action, _onFinish, _allowMultipleInstances, _id );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& translator.Assignable<YXCell.YXCurve>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action<float>>(L, 5)&& translator.Assignable<System.Action<float>>(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    YXCell.YXCurve _curve = (YXCell.YXCurve)translator.GetObject(L, 1, typeof(YXCell.YXCurve));
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _timeParam = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 5);
                    System.Action<float> _onFinish = translator.GetDelegate<System.Action<float>>(L, 6);
                    bool _allowMultipleInstances = LuaAPI.lua_toboolean(L, 7);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedure( _curve, _startValue, _endValue, _timeParam, _action, _onFinish, _allowMultipleInstances );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& translator.Assignable<YXCell.YXCurve>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action<float>>(L, 5)&& translator.Assignable<System.Action<float>>(L, 6)) 
                {
                    YXCell.YXCurve _curve = (YXCell.YXCurve)translator.GetObject(L, 1, typeof(YXCell.YXCurve));
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _timeParam = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 5);
                    System.Action<float> _onFinish = translator.GetDelegate<System.Action<float>>(L, 6);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedure( _curve, _startValue, _endValue, _timeParam, _action, _onFinish );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<YXCell.YXCurve>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action<float>>(L, 5)) 
                {
                    YXCell.YXCurve _curve = (YXCell.YXCurve)translator.GetObject(L, 1, typeof(YXCell.YXCurve));
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _timeParam = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 5);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedure( _curve, _startValue, _endValue, _timeParam, _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 9&& translator.Assignable<YXCell.YXUtils.ProcedureType>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action<float>>(L, 5)&& translator.Assignable<System.Action<float>>(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 8)&& (LuaAPI.lua_isnil(L, 9) || LuaAPI.lua_type(L, 9) == LuaTypes.LUA_TSTRING)) 
                {
                    YXCell.YXUtils.ProcedureType _type;translator.Get(L, 1, out _type);
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _timeParam = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 5);
                    System.Action<float> _onFinish = translator.GetDelegate<System.Action<float>>(L, 6);
                    float _lerpThreshold = (float)LuaAPI.lua_tonumber(L, 7);
                    bool _allowMultipleInstances = LuaAPI.lua_toboolean(L, 8);
                    string _id = LuaAPI.lua_tostring(L, 9);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedure( _type, _startValue, _endValue, _timeParam, _action, _onFinish, _lerpThreshold, _allowMultipleInstances, _id );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 8&& translator.Assignable<YXCell.YXUtils.ProcedureType>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action<float>>(L, 5)&& translator.Assignable<System.Action<float>>(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 8)) 
                {
                    YXCell.YXUtils.ProcedureType _type;translator.Get(L, 1, out _type);
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _timeParam = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 5);
                    System.Action<float> _onFinish = translator.GetDelegate<System.Action<float>>(L, 6);
                    float _lerpThreshold = (float)LuaAPI.lua_tonumber(L, 7);
                    bool _allowMultipleInstances = LuaAPI.lua_toboolean(L, 8);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedure( _type, _startValue, _endValue, _timeParam, _action, _onFinish, _lerpThreshold, _allowMultipleInstances );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& translator.Assignable<YXCell.YXUtils.ProcedureType>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action<float>>(L, 5)&& translator.Assignable<System.Action<float>>(L, 6)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7)) 
                {
                    YXCell.YXUtils.ProcedureType _type;translator.Get(L, 1, out _type);
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _timeParam = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 5);
                    System.Action<float> _onFinish = translator.GetDelegate<System.Action<float>>(L, 6);
                    float _lerpThreshold = (float)LuaAPI.lua_tonumber(L, 7);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedure( _type, _startValue, _endValue, _timeParam, _action, _onFinish, _lerpThreshold );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& translator.Assignable<YXCell.YXUtils.ProcedureType>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action<float>>(L, 5)&& translator.Assignable<System.Action<float>>(L, 6)) 
                {
                    YXCell.YXUtils.ProcedureType _type;translator.Get(L, 1, out _type);
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _timeParam = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 5);
                    System.Action<float> _onFinish = translator.GetDelegate<System.Action<float>>(L, 6);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedure( _type, _startValue, _endValue, _timeParam, _action, _onFinish );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<YXCell.YXUtils.ProcedureType>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action<float>>(L, 5)) 
                {
                    YXCell.YXUtils.ProcedureType _type;translator.Get(L, 1, out _type);
                    float _startValue = (float)LuaAPI.lua_tonumber(L, 2);
                    float _endValue = (float)LuaAPI.lua_tonumber(L, 3);
                    float _timeParam = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 5);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedure( _type, _startValue, _endValue, _timeParam, _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YXCell.YXUtils.StartProcedure!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartProcedureUnscaled_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<YXCell.YXCurve>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Action<float>>(L, 3)&& translator.Assignable<System.Action<float>>(L, 4)&& (LuaAPI.lua_isnil(L, 5) || LuaAPI.lua_type(L, 5) == LuaTypes.LUA_TSTRING)) 
                {
                    YXCell.YXCurve _curve = (YXCell.YXCurve)translator.GetObject(L, 1, typeof(YXCell.YXCurve));
                    float _time = (float)LuaAPI.lua_tonumber(L, 2);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 3);
                    System.Action<float> _onFinish = translator.GetDelegate<System.Action<float>>(L, 4);
                    string _id = LuaAPI.lua_tostring(L, 5);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedureUnscaled( _curve, _time, _action, _onFinish, _id );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<YXCell.YXCurve>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Action<float>>(L, 3)&& translator.Assignable<System.Action<float>>(L, 4)) 
                {
                    YXCell.YXCurve _curve = (YXCell.YXCurve)translator.GetObject(L, 1, typeof(YXCell.YXCurve));
                    float _time = (float)LuaAPI.lua_tonumber(L, 2);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 3);
                    System.Action<float> _onFinish = translator.GetDelegate<System.Action<float>>(L, 4);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedureUnscaled( _curve, _time, _action, _onFinish );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<YXCell.YXCurve>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Action<float>>(L, 3)) 
                {
                    YXCell.YXCurve _curve = (YXCell.YXCurve)translator.GetObject(L, 1, typeof(YXCell.YXCurve));
                    float _time = (float)LuaAPI.lua_tonumber(L, 2);
                    System.Action<float> _action = translator.GetDelegate<System.Action<float>>(L, 3);
                    
                        var gen_ret = YXCell.YXUtils.StartProcedureUnscaled( _curve, _time, _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YXCell.YXUtils.StartProcedureUnscaled!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopProcedure_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _id = LuaAPI.lua_tostring(L, 1);
                    
                    YXCell.YXUtils.StopProcedure( _id );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartCoroutine_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.Collections.IEnumerator>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    System.Collections.IEnumerator _cr = (System.Collections.IEnumerator)translator.GetObject(L, 1, typeof(System.Collections.IEnumerator));
                    bool _allowMultipleInstances = LuaAPI.lua_toboolean(L, 2);
                    string _id = LuaAPI.lua_tostring(L, 3);
                    
                        var gen_ret = YXCell.YXUtils.StartCoroutine( _cr, _allowMultipleInstances, _id );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Collections.IEnumerator>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    System.Collections.IEnumerator _cr = (System.Collections.IEnumerator)translator.GetObject(L, 1, typeof(System.Collections.IEnumerator));
                    bool _allowMultipleInstances = LuaAPI.lua_toboolean(L, 2);
                    
                        var gen_ret = YXCell.YXUtils.StartCoroutine( _cr, _allowMultipleInstances );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.Collections.IEnumerator>(L, 1)) 
                {
                    System.Collections.IEnumerator _cr = (System.Collections.IEnumerator)translator.GetObject(L, 1, typeof(System.Collections.IEnumerator));
                    
                        var gen_ret = YXCell.YXUtils.StartCoroutine( _cr );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YXCell.YXUtils.StartCoroutine!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopCoroutine_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<System.Collections.IEnumerator>(L, 1)) 
                {
                    System.Collections.IEnumerator _cr = (System.Collections.IEnumerator)translator.GetObject(L, 1, typeof(System.Collections.IEnumerator));
                    
                    YXCell.YXUtils.StopCoroutine( _cr );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _id = LuaAPI.lua_tostring(L, 1);
                    
                    YXCell.YXUtils.StopCoroutine( _id );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<UnityEngine.Coroutine>(L, 1)) 
                {
                    UnityEngine.Coroutine _cr = (UnityEngine.Coroutine)translator.GetObject(L, 1, typeof(UnityEngine.Coroutine));
                    
                    YXCell.YXUtils.StopCoroutine( _cr );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to YXCell.YXUtils.StopCoroutine!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
