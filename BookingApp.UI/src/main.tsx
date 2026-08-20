import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { Toaster } from 'sonner'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
    <Toaster id="info" theme="system" position="bottom-right" richColors/>
    <Toaster id="delete" theme="system" position="top-center" richColors/>
  </StrictMode>
)
