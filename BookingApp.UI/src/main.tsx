import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { Toaster } from 'sonner'
import { Auth0Provider } from '@auth0/auth0-react'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Auth0Provider
      domain={import.meta.env.VITE_AUTH0_DOMAIN}
      clientId={import.meta.env.VITE_AUTH0_CLIENT_ID}
      authorizationParams={{ 
        redirect_uri: window.location.origin, 
        audience: import.meta.env.VITE_AUTH0_AUDIENCE
        }}
      cacheLocation="localstorage"
      useRefreshTokens={true}>
      <App />
      <Toaster id="info" theme="system" position="bottom-right" richColors />
      <Toaster id="delete" theme="system" position="top-center" richColors />
    </Auth0Provider>
  </StrictMode>
);