import React from 'react'
import {
  CAvatar,
  CBadge,
  CDropdown,
  CDropdownDivider,
  CDropdownHeader,
  CDropdownItem,
  CDropdownMenu,
  CDropdownToggle,
} from '@coreui/react'
import {
  cilClipboard,
  cilToggleOn,
} from '@coreui/icons'
import CIcon from '@coreui/icons-react'

import NotLoggedInAvatar from './../../assets/images/avatars/NotLoggedInAvatar.png'

const NotLoggedInAppHeaderDropdown = () => {
  return (
    <CDropdown variant="nav-item">
      <CDropdownToggle placement="bottom-end" className="py-0 pe-0" caret={false}>
        <CAvatar src={NotLoggedInAvatar} size="md" />
      </CDropdownToggle>
      <CDropdownMenu className="pt-0" placement="bottom-end">
        <CDropdownHeader className="bg-body-secondary fw-semibold mb-2">Account</CDropdownHeader>
        <CDropdownItem href="/login">
          <CIcon icon={cilToggleOn} className="me-2" />
          Log In
        </CDropdownItem>
        <CDropdownItem href="/register">
          <CIcon icon={cilClipboard} className="me-2" />
          Register
        </CDropdownItem>
      </CDropdownMenu>
    </CDropdown>
  )
}

export default NotLoggedInAppHeaderDropdown
