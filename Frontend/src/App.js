import './App.css';
import GameContextProvider from './contexts/GameContext';
import RouteElements from './RouteElements';
import { BrowserRouter } from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
      <GameContextProvider>
        <RouteElements />
      </GameContextProvider>
    </BrowserRouter>
    
  );
}

export default App;
