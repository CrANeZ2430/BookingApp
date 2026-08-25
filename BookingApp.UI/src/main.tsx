import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { Toaster } from 'sonner'
import { Auth0Provider } from '@auth0/auth0-react'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Auth0Provider
      domain="dev-crn.eu.auth0.com"
      clientId="cQNrjuiRNCFFJgvPeUIJSBUesyuESDlT"
      authorizationParams={{ 
        redirect_uri: window.location.origin, 
        audience: "http://localhost:8080"
        }}>
      <App />
      <Toaster id="info" theme="system" position="bottom-right" richColors/>
      <Toaster id="delete" theme="system" position="top-center" richColors/>
    </Auth0Provider>
  </StrictMode>
);