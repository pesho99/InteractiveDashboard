import { NavLink } from "react-router-dom";

const Navbar: React.FC = () => {
    return (
      <nav>
        <ul style={navStyles}>
          <li>
            <NavLink to="/login" style={({ isActive }) => (isActive ? activeLinkStyle : linkStyle)}>
              Login
            </NavLink>
          </li>
          <li>
            <NavLink to="/register" style={({ isActive }) => (isActive ? activeLinkStyle : linkStyle)}>
              Register
            </NavLink>
          </li>
          <li>
            <NavLink to="/dashboard" style={({ isActive }) => (isActive ? activeLinkStyle : linkStyle)}>
              Dashboard
            </NavLink>
          </li>
        </ul>
      </nav>
    );
  };
  
  // Basic inline styles for navigation (you can replace it with a CSS file)
  const navStyles = {
    display: 'flex',
    listStyle: 'none',
    padding: '10px',
    backgroundColor: '#333',
    color: 'white',
    justifyContent: 'space-around'
  };
  
  const linkStyle = {
    color: 'white',
    textDecoration: 'none',
  };
  
  const activeLinkStyle = {
    fontWeight: 'bold',
    color: 'yellow',
  };
  
  export default Navbar;