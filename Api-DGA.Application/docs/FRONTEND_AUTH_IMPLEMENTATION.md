# README - Implementación de Frontend para Autenticación JWT

## 📋 Descripción del Proyecto

Este proyecto es una API REST desarrollada en **ASP.NET Core 8** con autenticación JWT que gestiona productos, clientes y ventas. El frontend debe implementar un sistema de autenticación completo que consuma los endpoints de la API.

## 🔗 Endpoints de Autenticación Disponibles

### **Base URL**: `https://localhost:7067`

### **1. Login**
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@api-dga.com",
  "password": "Admin123!"
}
```

**Respuesta exitosa (200):**
```json
{
  "success": true,
  "message": "Login exitoso",
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "refresh_token_here",
  "expiresAt": "2024-01-15T10:30:00Z",
  "user": {
    "id": 1,
    "name": "Administrador",
    "email": "admin@api-dga.com",
    "roles": ["Administrador"]
  }
}
```

### **2. Registro**
```http
POST /api/auth/register
Content-Type: application/json

{
  "name": "Nuevo Usuario",
  "email": "usuario@ejemplo.com",
  "password": "Contraseña123!",
  "confirmPassword": "Contraseña123!"
}
```

### **3. Renovar Token**
```http
POST /api/auth/refresh-token
Content-Type: application/json

{
  "refreshToken": "refresh_token_here"
}
```

### **4. Validar Token**
```http
GET /api/auth/validate
Authorization: Bearer {accessToken}
```

### **5. Revocar Token**
```http
POST /api/auth/revoke-token
Authorization: Bearer {accessToken}
Content-Type: application/json

"refresh_token_here"
```

## 🔐 Endpoints Protegidos

### **Productos**
- `GET /api/products` - Listar productos
- `GET /api/products/{id}` - Obtener producto por ID
- `POST /api/products` - Crear producto
- `PUT /api/products/{id}` - Actualizar producto
- `DELETE /api/products/{id}` - Eliminar producto

### **Clientes**
- `GET /api/clients` - Listar clientes
- `GET /api/clients/{id}` - Obtener cliente por ID
- `POST /api/clients` - Crear cliente
- `PUT /api/clients/{id}` - Actualizar cliente
- `DELETE /api/clients/{id}` - Eliminar cliente

### **Ventas**
- `GET /api/sales` - Listar ventas
- `GET /api/sales/{id}` - Obtener venta por ID
- `POST /api/sales` - Crear venta
- `PUT /api/sales/{id}` - Actualizar venta
- `DELETE /api/sales/{id}` - Eliminar venta

## 🛠️ Requisitos del Frontend

### **Tecnologías Recomendadas**
- **React** con TypeScript
- **Vue.js** con TypeScript
- **Angular**
- **Svelte**

### **Funcionalidades Requeridas**

#### **1. Páginas de Autenticación**
- **Login Page** (`/login`)
  - Formulario con email y contraseña
  - Validación de campos
  - Manejo de errores
  - Redirección automática si ya está autenticado

- **Register Page** (`/register`)
  - Formulario con nombre, email, contraseña y confirmación
  - Validación de contraseñas coincidentes
  - Validación de email único

#### **2. Gestión de Estado de Autenticación**
- **Context/Store** para manejar:
  - Estado de autenticación (isAuthenticated)
  - Información del usuario (user)
  - Tokens (accessToken, refreshToken)
  - Persistencia en localStorage/sessionStorage

#### **3. Interceptor HTTP**
- **Axios Interceptor** o similar para:
  - Agregar automáticamente `Authorization: Bearer {token}` a todas las peticiones
  - Manejar errores 401 (token expirado)
  - Renovar automáticamente el token usando refreshToken
  - Redirigir a login si no hay refreshToken válido

#### **4. Protección de Rutas**
- **Route Guards** para:
  - Proteger rutas que requieren autenticación
  - Redirigir a login si no está autenticado
  - Verificar roles para rutas específicas

#### **5. Componentes de UI**
- **Navbar/Header** con:
  - Información del usuario logueado
  - Botón de logout
  - Menú de navegación

- **Loading Spinner** para peticiones
- **Error Messages** para mostrar errores de autenticación

## 📁 Estructura de Archivos Recomendada

```
src/
├── components/
│   ├── auth/
│   │   ├── LoginForm.tsx
│   │   ├── RegisterForm.tsx
│   │   └── AuthLayout.tsx
│   ├── common/
│   │   ├── Navbar.tsx
│   │   ├── LoadingSpinner.tsx
│   │   └── ErrorMessage.tsx
│   └── protected/
│       ├── Products/
│       ├── Clients/
│       └── Sales/
├── pages/
│   ├── LoginPage.tsx
│   ├── RegisterPage.tsx
│   ├── DashboardPage.tsx
│   ├── ProductsPage.tsx
│   ├── ClientsPage.tsx
│   └── SalesPage.tsx
├── services/
│   ├── authService.ts
│   ├── apiService.ts
│   ├── productService.ts
│   ├── clientService.ts
│   └── saleService.ts
├── hooks/
│   ├── useAuth.ts
│   └── useApi.ts
├── context/
│   └── AuthContext.tsx
├── types/
│   ├── auth.ts
│   ├── product.ts
│   ├── client.ts
│   └── sale.ts
└── utils/
    ├── constants.ts
    └── helpers.ts
```

## 💻 Implementación Detallada

### **1. Tipos TypeScript**

```typescript
// types/auth.ts
export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  name: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface AuthResponse {
  success: boolean;
  message: string;
  accessToken: string;
  refreshToken: string;
  expiresAt: string;
  user: UserInfo;
}

export interface UserInfo {
  id: number;
  name: string;
  email: string;
  roles: string[];
}

export interface AuthState {
  isAuthenticated: boolean;
  user: UserInfo | null;
  accessToken: string | null;
  refreshToken: string | null;
  loading: boolean;
}
```

### **2. Servicio de Autenticación**

```typescript
// services/authService.ts
import axios from 'axios';

const API_BASE_URL = 'https://localhost:7067/api';

export class AuthService {
  static async login(credentials: LoginRequest): Promise<AuthResponse> {
    const response = await axios.post(`${API_BASE_URL}/auth/login`, credentials);
    return response.data;
  }

  static async register(userData: RegisterRequest): Promise<AuthResponse> {
    const response = await axios.post(`${API_BASE_URL}/auth/register`, userData);
    return response.data;
  }

  static async refreshToken(refreshToken: string): Promise<AuthResponse> {
    const response = await axios.post(`${API_BASE_URL}/auth/refresh-token`, {
      refreshToken
    });
    return response.data;
  }

  static async validateToken(token: string): Promise<boolean> {
    try {
      await axios.get(`${API_BASE_URL}/auth/validate`, {
        headers: { Authorization: `Bearer ${token}` }
      });
      return true;
    } catch {
      return false;
    }
  }

  static async logout(refreshToken: string): Promise<void> {
    await axios.post(`${API_BASE_URL}/auth/revoke-token`, refreshToken);
  }
}
```

### **3. Context de Autenticación**

```typescript
// context/AuthContext.tsx
import React, { createContext, useContext, useReducer, useEffect } from 'react';
import { AuthService } from '../services/authService';

interface AuthContextType {
  state: AuthState;
  login: (credentials: LoginRequest) => Promise<void>;
  register: (userData: RegisterRequest) => Promise<void>;
  logout: () => void;
  refreshToken: () => Promise<void>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [state, dispatch] = useReducer(authReducer, initialState);

  const login = async (credentials: LoginRequest) => {
    try {
      dispatch({ type: 'AUTH_START' });
      const response = await AuthService.login(credentials);
      
      // Guardar en localStorage
      localStorage.setItem('accessToken', response.accessToken);
      localStorage.setItem('refreshToken', response.refreshToken);
      localStorage.setItem('user', JSON.stringify(response.user));
      
      dispatch({ 
        type: 'AUTH_SUCCESS', 
        payload: { 
          user: response.user, 
          accessToken: response.accessToken, 
          refreshToken: response.refreshToken 
        } 
      });
    } catch (error) {
      dispatch({ type: 'AUTH_ERROR', payload: error.message });
      throw error;
    }
  };

  const logout = () => {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('user');
    dispatch({ type: 'AUTH_LOGOUT' });
  };

  // Verificar token al cargar la app
  useEffect(() => {
    const token = localStorage.getItem('accessToken');
    const user = localStorage.getItem('user');
    
    if (token && user) {
      dispatch({ 
        type: 'AUTH_SUCCESS', 
        payload: { 
          user: JSON.parse(user), 
          accessToken: token, 
          refreshToken: localStorage.getItem('refreshToken') 
        } 
      });
    }
  }, []);

  return (
    <AuthContext.Provider value={{ state, login, register, logout, refreshToken }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};
```

### **4. Interceptor HTTP**

```typescript
// services/apiService.ts
import axios from 'axios';
import { AuthService } from './authService';

const API_BASE_URL = 'https://localhost:7067/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000,
});

// Request interceptor
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('accessToken');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        const refreshToken = localStorage.getItem('refreshToken');
        if (refreshToken) {
          const response = await AuthService.refreshToken(refreshToken);
          
          localStorage.setItem('accessToken', response.accessToken);
          localStorage.setItem('refreshToken', response.refreshToken);
          
          originalRequest.headers.Authorization = `Bearer ${response.accessToken}`;
          return api(originalRequest);
        }
      } catch (refreshError) {
        // Refresh token expirado, redirigir a login
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('user');
        window.location.href = '/login';
      }
    }

    return Promise.reject(error);
  }
);

export default api;
```

### **5. Componente de Login**

```typescript
// components/auth/LoginForm.tsx
import React, { useState } from 'react';
import { useAuth } from '../../context/AuthContext';
import { useNavigate } from 'react-router-dom';

export const LoginForm: React.FC = () => {
  const [credentials, setCredentials] = useState({ email: '', password: '' });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      await login(credentials);
      navigate('/dashboard');
    } catch (error) {
      setError(error.response?.data?.message || 'Error al iniciar sesión');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-form">
      <h2>Iniciar Sesión</h2>
      {error && <div className="error-message">{error}</div>}
      
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Email:</label>
          <input
            type="email"
            value={credentials.email}
            onChange={(e) => setCredentials({...credentials, email: e.target.value})}
            required
          />
        </div>
        
        <div className="form-group">
          <label>Contraseña:</label>
          <input
            type="password"
            value={credentials.password}
            onChange={(e) => setCredentials({...credentials, password: e.target.value})}
            required
          />
        </div>
        
        <button type="submit" disabled={loading}>
          {loading ? 'Iniciando sesión...' : 'Iniciar Sesión'}
        </button>
      </form>
    </div>
  );
};
```

### **6. Route Guard**

```typescript
// components/ProtectedRoute.tsx
import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

interface ProtectedRouteProps {
  children: React.ReactNode;
  requiredRoles?: string[];
}

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ 
  children, 
  requiredRoles = [] 
}) => {
  const { state } = useAuth();
  const location = useLocation();

  if (!state.isAuthenticated) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  if (requiredRoles.length > 0) {
    const hasRequiredRole = requiredRoles.some(role => 
      state.user?.roles.includes(role)
    );
    
    if (!hasRequiredRole) {
      return <Navigate to="/unauthorized" replace />;
    }
  }

  return <>{children}</>;
};
```

## 🚀 Instrucciones de Implementación

### **1. Configurar el Proyecto**
```bash
# Crear proyecto React con TypeScript
npx create-react-app frontend --template typescript

# O con Vite
npm create vite@latest frontend -- --template react-ts

cd frontend
npm install axios react-router-dom
```

### **2. Configurar Rutas**
```typescript
// App.tsx
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import { ProtectedRoute } from './components/ProtectedRoute';
import { LoginPage } from './pages/LoginPage';
import { DashboardPage } from './pages/DashboardPage';

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route 
            path="/dashboard" 
            element={
              <ProtectedRoute>
                <DashboardPage />
              </ProtectedRoute>
            } 
          />
          <Route 
            path="/products" 
            element={
              <ProtectedRoute requiredRoles={['Administrador']}>
                <ProductsPage />
              </ProtectedRoute>
            } 
          />
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}
```

### **3. Probar la Implementación**

1. **Iniciar la API**: `dotnet run` en el proyecto .NET
2. **Iniciar el Frontend**: `npm start` en el proyecto React
3. **Navegar a**: `http://localhost:3000/login`
4. **Credenciales de prueba**:
   - Email: `admin@api-dga.com`
   - Contraseña: `Admin123!`

## 📝 Notas Importantes

- **CORS**: La API ya está configurada para permitir peticiones desde `http://localhost:3000`
- **HTTPS**: En desarrollo, la API usa HTTPS (`https://localhost:7067`)
- **Tokens**: Los tokens JWT expiran en 60 minutos
- **Refresh**: El refresh token expira en 7 días
- **Roles**: El usuario administrador tiene el rol "Administrador"

## 🔧 Solución de Problemas

### **Error de CORS**
Si hay problemas de CORS, verificar que la API esté configurada correctamente en `Program.cs`.

### **Token Expirado**
El interceptor HTTP maneja automáticamente la renovación de tokens.

### **Error 401**
Verificar que el token se esté enviando correctamente en el header `Authorization`.

### **Error de Certificado HTTPS**
En desarrollo, puede ser necesario aceptar el certificado HTTPS de desarrollo:
```bash
# En Windows
dotnet dev-certs https --trust

# En macOS
dotnet dev-certs https --trust

# En Linux
dotnet dev-certs https --trust
```

## 📚 Recursos Adicionales

- [React Router Documentation](https://reactrouter.com/)
- [Axios Documentation](https://axios-http.com/)
- [TypeScript Handbook](https://www.typescriptlang.org/docs/)
- [JWT.io](https://jwt.io/) - Para debuggear tokens JWT

## 🤝 Contribución

Para contribuir al proyecto:

1. Fork el repositorio
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

---

**¡Con esta implementación tendrás un sistema de autenticación completo y funcional!** 🎉

## 📞 Soporte

Si tienes alguna pregunta o necesitas ayuda con la implementación, puedes:

- Crear un issue en el repositorio
- Contactar al equipo de desarrollo
- Revisar la documentación de la API

---

**Última actualización**: Enero 2024
**Versión**: 1.0.0
