import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom';
import App from './App.tsx'
import './index.css'
import { SnackbarProvider } from 'notistack';

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <SnackbarProvider
        anchorOrigin={{
          vertical: 'bottom',
          horizontal: 'right'
        }}
        maxSnack={3}
        dense
      >
        <App />
      </SnackbarProvider>
    </BrowserRouter>
  </StrictMode>,
)
